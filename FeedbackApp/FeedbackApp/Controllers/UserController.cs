using FeedbackApp.Data;
using FeedbackApp.Entities;
using FeedbackApp.Models.StoredFunctionsAndProcedures;
using FeedbackApp.Models.Users;
using FeedbackApp.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly FeedbackAppDbContext _context;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users-with-project")]
        public async Task<IActionResult> GetUsersWithProjects()
        {
            var usersWithProjects = await _userRepository.GetUsersWithProjectsAsync();

            var userDtos = usersWithProjects.Select(u => new UserWithProjectDto
            {
                Id_Uzytkownika = u.Id_Uzytkownika,
                Imie = u.Imie,
                Nazwisko = u.Nazwisko,
                Id_Projektu = u.Id_Projektu,
                Nazwa_Projektu = u.Nazwa_Projektu
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }).ToList();

            return Ok(userDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDetail = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return Ok(userDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto is null)
            {
                return BadRequest("User data is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(createUserDto.Email);
            if (existingUser is not null)
            {
                return Conflict("Email is already in use");
            }

            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Password
            };

            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, createUserDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.PasswordHash = updateUserDto.Password;

            await _userRepository.UpdateUserAsync(user);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }


    }
}

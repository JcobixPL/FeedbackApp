using FeedbackApp.Entities;
using FeedbackApp.Models.Feedbacks;
using FeedbackApp.Models.Projects;
using FeedbackApp.Models.StoredFunctionsAndProcedures;
using FeedbackApp.Repositories.Projects;
using FeedbackApp.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FeedbackApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public ProjectController(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();

            var projectDtos = projects.Select(project => new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerId = project.OwnerId,
                CreatedAt = project.CreatedAt,
                Feedbacks = project.Feedbacks.Select(feedback => new FeedbackDto
                {
                    Id = feedback.Id,
                    Type = feedback.Type,
                    Priority = feedback.Priority,
                    Status = feedback.Status,
                    CreatedAt = feedback.CreatedAt,
                    Description = feedback.Description,
                    UserId = feedback.UserId,
                    ProjectId = feedback.ProjectId
                }).ToList()
            }).ToList();

            return Ok(projectDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var projectDetail = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerId = project.OwnerId,
                CreatedAt = project.CreatedAt,
                Feedbacks = project.Feedbacks.Select(feedback => new FeedbackDto
                {
                    Id = feedback.Id,
                    Type = feedback.Type,
                    Priority = feedback.Priority,
                    Status = feedback.Status,
                    CreatedAt = feedback.CreatedAt,
                    Description = feedback.Description,
                    UserId = feedback.UserId,
                    ProjectId = feedback.ProjectId
                }).ToList()
            };

            return Ok(projectDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectDto createProjectDto)
        {
            if (createProjectDto == null)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(int.Parse(userId));

            var project = new Project
            {
                Name = createProjectDto.Name,
                Description = createProjectDto.Description,
                OwnerId = int.Parse(userId),
                CreatedAt = DateTime.UtcNow,
                User = user
            };

            await _projectRepository.AddProjectAsync(project);

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerId = project.OwnerId,
                CreatedAt = project.CreatedAt
            };

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, projectDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project is null)
            {
                return NotFound();
            }

            project.Name = updateProjectDto.Name;
            project.Description = updateProjectDto.Description;

            await _projectRepository.UpdateProjectAsync(project);
            return Ok(updateProjectDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectRepository.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}

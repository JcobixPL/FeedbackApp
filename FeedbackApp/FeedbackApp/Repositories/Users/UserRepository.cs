using FeedbackApp.Data;
using FeedbackApp.Entities;
using FeedbackApp.Models.StoredFunctionsAndProcedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly FeedbackAppDbContext _context;

        public UserRepository(FeedbackAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserWithProjectDto>> GetUsersWithProjectsAsync()
        {
            var result = await _context.UsersWithProjects
                .FromSqlRaw("SELECT * FROM GetUsersWithProjects()")
                .ToListAsync();

            return result;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

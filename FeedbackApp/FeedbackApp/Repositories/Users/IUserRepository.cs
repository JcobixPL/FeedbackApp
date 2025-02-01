using FeedbackApp.Entities;
using FeedbackApp.Models.StoredFunctionsAndProcedures;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<UserWithProjectDto>> GetUsersWithProjectsAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
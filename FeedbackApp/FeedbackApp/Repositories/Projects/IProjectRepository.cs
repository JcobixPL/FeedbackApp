using FeedbackApp.Entities;
using FeedbackApp.Models.StoredFunctionsAndProcedures;

namespace FeedbackApp.Repositories.Projects
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<List<Project>> GetAllProjectsAsync();
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }
}

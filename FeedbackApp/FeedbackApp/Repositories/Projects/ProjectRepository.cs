using FeedbackApp.Data;
using FeedbackApp.Entities;
using FeedbackApp.Models.StoredFunctionsAndProcedures;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Repositories.Projects
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly FeedbackAppDbContext _context;

        public ProjectRepository(FeedbackAppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.Include(p => p.Feedbacks).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.Feedbacks)
                .ToListAsync();
        }

        public async Task AddProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await GetProjectByIdAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}

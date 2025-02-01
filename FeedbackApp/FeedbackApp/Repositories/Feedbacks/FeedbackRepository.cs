using FeedbackApp.Data;
using FeedbackApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Repositories.Feedbacks
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackAppDbContext _context;

        public FeedbackRepository(FeedbackAppDbContext context)
        {
            _context = context;
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks.Include(f => f.Comments).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await GetFeedbackByIdAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}

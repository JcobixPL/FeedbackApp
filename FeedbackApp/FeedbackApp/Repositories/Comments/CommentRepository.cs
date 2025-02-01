using FeedbackApp.Data;
using FeedbackApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedbackApp.Repositories.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly FeedbackAppDbContext _context;

        public CommentRepository(FeedbackAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetCommentsByFeedbackIdAsync(int feedbackId)
        {
            return await _context.Comments.Where(c => c.FeedbackId == feedbackId).ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await GetCommentByIdAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}

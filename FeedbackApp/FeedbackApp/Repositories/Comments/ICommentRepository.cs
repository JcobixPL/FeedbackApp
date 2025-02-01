using FeedbackApp.Entities;

namespace FeedbackApp.Repositories.Comments
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task<List<Comment>> GetCommentsByFeedbackIdAsync(int feedbackId);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}

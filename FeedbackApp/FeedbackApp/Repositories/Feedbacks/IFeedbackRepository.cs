using FeedbackApp.Entities;

namespace FeedbackApp.Repositories.Feedbacks
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetFeedbackByIdAsync(int id);
        Task<List<Feedback>> GetAllFeedbacksAsync();
        Task AddFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(int id);
    }
}

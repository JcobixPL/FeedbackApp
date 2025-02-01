using FeedbackApp.Entities;
using FeedbackApp.Models.Comments;

namespace FeedbackApp.Models.Feedbacks
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public FeedbackType Type { get; set; }
        public FeedbackPriority Priority { get; set; }
        public FeedbackStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Description { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}

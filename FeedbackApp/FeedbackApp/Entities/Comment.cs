using FeedbackApp.Models.Feedbacks;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackApp.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int FeedbackId { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("UserId")]
        public required User User { get; set; }
        [ForeignKey("FeedbackId")]
        public required Feedback Feedback { get; set; }

        public Comment() { }

        public Comment (int userId, int feedbackId, string text, DateTime createdAt, User user, Feedback feedback)
        {
            UserId = userId;
            FeedbackId = feedbackId;
            Text = text;
            CreatedAt = createdAt;
            User = user;
            Feedback = feedback;
        }
    }
}



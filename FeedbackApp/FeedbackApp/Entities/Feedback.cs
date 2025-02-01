using FeedbackApp.Models.Comments;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackApp.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public required FeedbackType Type { get; set; }
        public required FeedbackPriority Priority { get; set; }
        public required FeedbackStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string Description { get; set; }
        public required int UserId { get; set; }
        public required int ProjectId { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
        [ForeignKey("ProjectId")]
        public required Project Project { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Feedback() { }

        public Feedback(string description, DateTime createdAt, FeedbackType type, FeedbackStatus status, FeedbackPriority priority, int userId, int projectId, User user, Project project)
        {
            Description = description;
            CreatedAt = createdAt;
            Type = type;
            Status = status;
            Priority = priority;
            UserId = userId;
            ProjectId = projectId;
            User = user;
            Project = project;
        }
    }

    public enum FeedbackType
    {
        Bug,
        Suggestion
    }

    public enum FeedbackPriority
    {
        Low,
        Medium,
        High
    }

    public enum FeedbackStatus
    {
        New,
        InProgress,
        Solved
    }
}

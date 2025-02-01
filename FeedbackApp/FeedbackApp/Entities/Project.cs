using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackApp.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("OwnerId")]
        public required User User { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public Project(string name, string description, int ownerId, DateTime createdAt, User user)
        {
            Name = name;
            Description = description;
            OwnerId = ownerId;
            CreatedAt = createdAt;
            User = user;
        }

        public Project() { }
    }
}

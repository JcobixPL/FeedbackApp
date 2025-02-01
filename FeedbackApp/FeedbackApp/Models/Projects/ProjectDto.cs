using FeedbackApp.Models.Feedbacks;

namespace FeedbackApp.Models.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<FeedbackDto> Feedbacks { get; set; } = new List<FeedbackDto>();
    }
}

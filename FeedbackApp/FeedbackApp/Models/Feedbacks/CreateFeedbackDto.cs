using FeedbackApp.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FeedbackApp.Models.Feedbacks
{
    public class CreateFeedbackDto
    {

        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
        public string Description { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}

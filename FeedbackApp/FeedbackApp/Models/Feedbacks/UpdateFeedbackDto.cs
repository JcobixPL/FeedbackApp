using FeedbackApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Models.Feedbacks
{
    public class UpdateFeedbackDto
    {
        public required string Description { get; set; }
    }
}

namespace FeedbackApp.Models.Comments
{
    public class CreateCommentDto
    {
        public required int FeedbackId { get; set; }
        public required string Text { get; set; }
    }
}

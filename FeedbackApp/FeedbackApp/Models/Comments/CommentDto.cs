namespace FeedbackApp.Models.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FeedbackId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

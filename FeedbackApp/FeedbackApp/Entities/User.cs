using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FeedbackApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        [JsonIgnore]
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        [JsonIgnore]
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        
        [JsonIgnore] public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public User(string firstName, string lastName, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public User() { }
    }
}

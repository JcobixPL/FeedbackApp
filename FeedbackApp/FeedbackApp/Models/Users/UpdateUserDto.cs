using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Models.Users
{
    public class UpdateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit."
        )]
        public required string Password { get; set; } 
    }

}

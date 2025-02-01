using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Models.Users
{
    public class CreateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit."
        )]
        public required string Password { get; set; } 
    }

}

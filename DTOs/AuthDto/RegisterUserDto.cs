using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.DTOs.AuthDto
{
    public record RegisterUserDto(
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        string Name,

        [Required]
        [MinLength(3, ErrorMessage = "Your last name must be at least 5 characters")]
        string LastName,

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        string Email,

        [Required]
        string Password
    );
}
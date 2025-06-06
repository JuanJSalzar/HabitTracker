using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.DTOs.UpdateDto
{
    public record UpdateUserDto(
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        string Name,

        [Required]
        [MinLength(3, ErrorMessage = "Your last name must be at least 5 characters")]
        string LastName,

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        string Email
    );
}
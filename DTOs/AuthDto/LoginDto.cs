using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.DTOs.AuthDto
{
    public record LoginDto(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password
    );
}
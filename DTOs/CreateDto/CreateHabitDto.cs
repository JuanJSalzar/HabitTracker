using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.DTOs.CreateDto
{
    public record CreateHabitDto(
        [Required]
        [MinLength(3, ErrorMessage = "Habit name must be at least 3 characters")]
        string Name,

        string? Description,

        CreateHabitLogDto? CurrentLog // Include habit log data when creating the habit
    );
}
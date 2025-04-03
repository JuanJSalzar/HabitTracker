using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.DTOs.UpdateDto
{
    public record UpdateHabitDto(
        [Required]
        [MinLength(3, ErrorMessage = "Habit name must be at least 3 characters")]
        string Name,

        string? Description,

        UpdateHabitLogDto? CurrentLog // Optional: include updated habit log data
    );
}
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Models;


namespace HabitsTracker.DTOs.CreateDto
{
    public record CreateHabitDto(
        [Required]
        [MinLength(3, ErrorMessage = "Habit name must be at least 3 characters")]
        string Name,

        string? Description,

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "HabitId must be a positive number")]
        int UserId,

        CreateHabitLogDto? CurrentLog // Optional: include habit log data when creating the habit
    );
}
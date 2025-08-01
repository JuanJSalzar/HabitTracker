using HabitsTracker.Models;

namespace HabitsTracker.DTOs.CreateDto
{
    public record CreateHabitLogDto(
        Status IsCompleted,
        string? Notes,
        DateTime StartTime,
        TimeSpan? Duration
    );
}
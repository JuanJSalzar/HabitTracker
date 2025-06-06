using HabitsTracker.Models;

namespace HabitsTracker.DTOs.UpdateDto
{
    public record UpdateHabitLogDto(
        Status IsCompleted,
        string? Notes,
        DateTime StartTime,
        TimeSpan? Duration
    );
}
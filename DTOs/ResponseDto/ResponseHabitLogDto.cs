using HabitsTracker.Models;

namespace HabitsTracker.DTOs.ResponseDto
{
    public record ResponseHabitLogDto(
        Status IsCompleted,
        string? Notes,
        DateTime StartTime,
        TimeSpan? Duration
    );
}
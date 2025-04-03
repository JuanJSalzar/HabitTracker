
using HabitsTracker.Models;

namespace HabitsTracker.DTOs.ResponseDto
{
    public record ResponseHabitDto(
        int Id,
        string Name,
        string? Description,
        int UserId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        ResponseHabitLogDto? CurrentLog
    );
}
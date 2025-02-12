using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
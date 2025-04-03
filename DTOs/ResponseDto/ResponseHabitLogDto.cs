using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
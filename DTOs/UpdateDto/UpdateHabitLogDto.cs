using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.Models;

namespace HabitsTracker.DTOs.ResponseDto
{
    public record ResponseUserDto(
        int Id,
        string Name,
        string LastName,
        string Email,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
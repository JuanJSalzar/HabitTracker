using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsTracker.DTOs.AuthDto
{
    public record AuthResultDto(
        string Token,
        DateTime ExpiresAt
    );
}
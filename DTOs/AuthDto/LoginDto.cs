using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsTracker.DTOs.AuthDto
{
    public record LoginDto(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password
    );
}
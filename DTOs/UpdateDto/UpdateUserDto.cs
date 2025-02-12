using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.Models;

namespace HabitsTracker.DTOs.UpdateDto
{
    public record UpdateUserDto(
    int Id,

    [property: MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    string Name,

    [property: MinLength(5, ErrorMessage = "Your last name must be at least 5 characters")]
    string LastName,

    [property: EmailAddress(ErrorMessage = "Invalid email format")]
    [property: RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
    string Email
);

}
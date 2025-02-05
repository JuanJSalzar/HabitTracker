using System;
using System.ComponentModel.DataAnnotations;
using Azure.Core;

namespace HabitsTracker.Models
{
    public class User : BaseEntity
    {
        [MinLength(3, ErrorMessage = $"Your name must be greater than 3 letters")]
        public required string Name { get; set; }
        [MinLength(3, ErrorMessage = $"Your last name must be greater than 5 letters")]
        public required string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }
        public ICollection<Habit> Habits { get; } = [];
    }
}
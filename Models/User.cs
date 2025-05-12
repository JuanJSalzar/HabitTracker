using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HabitsTracker.Models
{
    public class User : IdentityUser<int>
    {
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        public required string Name { get; set; }
        [MinLength(3, ErrorMessage = "Your last name must be at least 5 characters")]
        public required string LastName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Habit> Habits { get; } = [];
    }
}
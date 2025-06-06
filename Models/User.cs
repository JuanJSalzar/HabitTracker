using Microsoft.AspNetCore.Identity;

namespace HabitsTracker.Models
{
    public class User : IdentityUser<int>
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Habit> Habits { get; } = [];
    }
}
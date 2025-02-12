using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Models
{
    public class Habit : BaseEntity
    {
        [MinLength(3, ErrorMessage = "Habit name must be at least 3 characters")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public HabitLog? CurrentLog { get; set; }
    }
}
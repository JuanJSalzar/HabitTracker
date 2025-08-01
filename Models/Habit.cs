using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Models
{
    public class Habit : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public HabitLog? CurrentLog { get; set; }
    }
}
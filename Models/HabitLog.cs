namespace HabitsTracker.Models
{
    public class HabitLog
    {
        public required Status IsCompleted { get; set; }
        public string? Notes { get; set; }  // Additional context
        public DateTime StartTime { get; set; }
        public TimeSpan? Duration { get; set; }  // Track how long someone spend on a habit.
    }

    public enum Status
    {
        Uncompleted,
        OnGoing,
        Completed,
        Pending,
    }
}
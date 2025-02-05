namespace HabitsTracker.Models
{
    public class HabitLog
    {
        public required Status IsCompleted { get; set; }
        public string? Notes { get; set; }  // Optional: Additional context
        public DateTime StartTime { get; set; }
        public TimeSpan? Duration { get; set; }  // Optional: Track how long someone spent on a habit.
    }

    public enum Status
    {
        Uncompleted,
        OnGoing,
        Completed,
        Pending,
    }
}
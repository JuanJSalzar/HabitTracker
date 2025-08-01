namespace HabitsTracker.Models
{
    public class HabitLog
    {
        public required Status IsCompleted { get; set; }
        public string? Notes { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
    }

    public enum Status
    {
        Uncompleted,
        OnGoing,
        Completed,
        Pending,
    }
}
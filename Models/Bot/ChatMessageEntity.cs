namespace HabitsTracker.Models.Bot;

public class ChatMessageEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public string Role { get; set; } = string.Empty; // "user" or "assistant"
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

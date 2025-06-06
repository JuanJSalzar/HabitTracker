namespace HabitsTracker.Models.Bot;

public class Message
{
    public Message() { }

    public Message(string prompt)
    {
        Prompt = prompt;
    }
    public string? Prompt { get; set; }
    
}
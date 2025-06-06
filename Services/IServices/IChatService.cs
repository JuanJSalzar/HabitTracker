namespace HabitsTracker.Services.IServices;

public interface IChatService
{
    Task<string> GetResponse(string prompt, int userId);
}
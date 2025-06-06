using HabitsTracker.Models.Bot;
using HabitsTracker.Services.ServicesImplementation;

namespace HabitsTracker.Services.IServices;

public interface IChatService
{
    Task<string> GetResponse(string prompt, int userId);
}
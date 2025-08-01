using HabitsTracker.Models.Bot;

namespace HabitsTracker.Repository.GenericRepository;

public interface IChatMessageRepository
{
    Task<List<ChatMessageEntity>> GetUserMessagesAsync(int userId);
    Task<int> CountUserMessagesAsync(int userId, DateTime from);
}
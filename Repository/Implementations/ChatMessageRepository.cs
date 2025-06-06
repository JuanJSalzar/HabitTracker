using HabitsTracker.Data;
using HabitsTracker.Models;
using HabitsTracker.Models.Bot;
using HabitsTracker.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HabitsTracker.Repository.Implementations;

public class ChatMessageRepository(HabitTrackerContext context) : GenericRepository<ChatMessageRepository>(context), IChatMessageRepository
{
    private readonly HabitTrackerContext _context = context;
    public async Task<List<ChatMessageEntity>> GetUserMessagesAsync(int userId)
    {
        return await _context.ChatMessages
            .AsNoTracking()
            .Where(m => m.UserId == userId)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
    }

    public async Task<int> CountUserMessagesAsync(int userId)
    {
        return await _context.ChatMessages
            .AsNoTracking()
            .CountAsync(m => m.UserId == userId && m.Role == "user");
    }
}
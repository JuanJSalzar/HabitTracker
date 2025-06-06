using HabitsTracker.Data;
using HabitsTracker.Models;
using Microsoft.EntityFrameworkCore;
using HabitsTracker.Repository.GenericRepository;

namespace HabitsTracker.Repository.Implementations
{
    public class HabitRepository(HabitTrackerContext context) : GenericRepository<Habit>(context), IHabitRepository
    {
        private readonly HabitTrackerContext _context = context;
        public int CountPendingHabitsByUser(int userId)
        {
            return _context.Habits.Count(h => h.UserId == userId && h.CurrentLog != null && h.CurrentLog.IsCompleted == Status.Pending);
        }

        public IQueryable<Habit> GetHabitsFromUser(int userId)
        {
            return _context.Habits.Where(h => h.UserId == userId).AsNoTracking();
        }

        public Task<Habit?> GetHabitFromUserAndId(int userId, int habitId)
        {
            return _context.Habits.AsNoTracking().FirstOrDefaultAsync(h => h.UserId == userId && h.Id == habitId);
        }
    }
}
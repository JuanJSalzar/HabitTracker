using HabitsTracker.Models;

namespace HabitsTracker.Repository.GenericRepository
{
    public interface IHabitRepository
    {
        int CountPendingHabitsByUser(int userId);
        IQueryable<Habit> GetHabitsFromUser(int userId);
        Task<Habit?> GetHabitFromUserAndId(int userId, int id);
    }
}
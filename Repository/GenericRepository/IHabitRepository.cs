using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
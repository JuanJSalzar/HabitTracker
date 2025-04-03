using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.Data;
using HabitsTracker.Models;
using HabitsTracker.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HabitsTracker.Repository.Implementations
{
    public class HabitRepository(HabitTrackerContext context) : GenericRepository<Habit>(context), IHabitRepository
    {
        private readonly HabitTrackerContext _context = context;
        public int CountPendingHabitsByUser(int userId)
        {
            return _context.Habits.Where(h => h.UserId == userId && h.CurrentLog != null && h.CurrentLog.IsCompleted == Status.Pending).Count();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.Data;
using HabitsTracker.Models;
using HabitsTracker.Repository.GenericRepository;

namespace HabitsTracker.Repository.Implementations
{
    public class HabitRepository(HabitTrackerContext context) : GenericRepository<Habit>(context)
    {
    }
}
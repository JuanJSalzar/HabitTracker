using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.Data;
using HabitsTracker.Models;

namespace HabitsTracker.Repository.Implementations
{
    public class UserRepository(HabitTrackerContext context) : GenericRepository<User>(context)
    {
    }
}
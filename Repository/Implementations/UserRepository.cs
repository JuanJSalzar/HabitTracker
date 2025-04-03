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
    public class UserRepository(HabitTrackerContext context) : GenericRepository<User>(context), IUserRepository
    {
        private readonly HabitTrackerContext _context = context;

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
using HabitsTracker.Data;
using HabitsTracker.Models;
using Microsoft.EntityFrameworkCore;
using HabitsTracker.Repository.GenericRepository;

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsTracker.Repository.GenericRepository
{
    public interface IUserRepository
    {
       Task<bool> ExistsByEmailAsync(string email);
    }
}
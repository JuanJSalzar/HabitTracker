namespace HabitsTracker.Repository.GenericRepository
{
    public interface IUserRepository
    {
       Task<bool> ExistsByEmailAsync(string email);
    }
}
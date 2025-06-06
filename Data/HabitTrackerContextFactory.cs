using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HabitsTracker.Data
{
    public class HabitTrackerContextFactory : IDesignTimeDbContextFactory<HabitTrackerContext>
    {
        public HabitTrackerContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<Program>()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String 'DefaultConnection' not found");

            var optionsBuilder = new DbContextOptionsBuilder<HabitTrackerContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new HabitTrackerContext(optionsBuilder.Options);
        }
    }
}
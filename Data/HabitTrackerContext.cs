using HabitsTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace HabitsTracker.Data
{
    public class HabitTrackerContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public HabitTrackerContext(DbContextOptions<HabitTrackerContext> options) : base(options) { }
        public DbSet<Habit> Habits { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Habit>(h =>
            {

                h.ToTable("Habit");
                h.HasKey(h => h.Id);
                h.Property(h => h.Name).IsRequired(true).HasMaxLength(100);
                h.Property(h => h.Description).IsRequired(false);
                h.Property(h => h.UpdatedAt).IsRequired(true).HasDefaultValueSql("GETDATE()");
                h.Property(h => h.CreatedAt).IsRequired(true).HasDefaultValueSql("GETDATE()");
                h.HasOne(u => u.User).WithMany(h => h.Habits).HasForeignKey(u => u.UserId);
            });

            modelBuilder.Entity<Habit>()
                .OwnsOne(h => h.CurrentLog, log =>
                {
                    log.Property(l => l.IsCompleted)
                    .HasConversion(
                        v => v.ToString(),
                        v => (Status)Enum.Parse(typeof(Status), v)
                    ).IsRequired(true);

                    log.Property(l => l.Duration).IsRequired(false)
                        .HasConversion(
                            hl => hl.HasValue ? hl.Value.Ticks : (long?)null,
                            hl => hl.HasValue ? TimeSpan.FromTicks(hl.Value) : null);
                    log.Property(l => l.Notes).IsRequired(false);
                });

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("User");
                u.HasKey(u => u.Id);
                u.Property(u => u.Name).IsRequired(true).HasMaxLength(100);
                u.Property(u => u.LastName).IsRequired(true).HasMaxLength(100);
                u.Property(u => u.Email).IsRequired(true).HasMaxLength(50);
                u.HasIndex(u => u.Email).IsUnique();
                u.Property(u => u.UpdatedAt).IsRequired(true).HasDefaultValueSql("GETDATE()");
                u.Property(u => u.CreatedAt).IsRequired(true).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
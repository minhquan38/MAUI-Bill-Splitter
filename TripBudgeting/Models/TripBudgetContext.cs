using Microsoft.EntityFrameworkCore;

namespace TripBudgeting.Models
{
    public class TripBudgetContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "TripBudget.db");
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>().Property(t => t.ImagePath).IsRequired(false);
        }
    }
}


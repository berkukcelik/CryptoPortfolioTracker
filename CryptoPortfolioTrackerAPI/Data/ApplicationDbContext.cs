using Microsoft.EntityFrameworkCore;
using CryptoPortfolioTrackerAPI.Models;
namespace CryptoPortfolioTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPortfolio> UserPortfolios { get; set; }

        public DbSet<CryptoCoin> CryptoCoins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CryptoCoin>()
                .HasIndex(c => c.ApiId)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Id)
                .IsUnique();
            
        }
    }
}
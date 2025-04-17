using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Context
{
    public class ApplicationDbConext : DbContext
    {
        public ApplicationDbConext() { }

        public ApplicationDbConext(DbContextOptions<ApplicationDbConext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(o => o.Role)
                .HasConversion<string>();
            modelBuilder.Entity<Account>()
                .Property(o => o.EnumAccountStatus)
                .HasConversion<string>();
        }
    }
}

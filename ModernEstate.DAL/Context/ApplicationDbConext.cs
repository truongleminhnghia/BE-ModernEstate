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
        public DbSet<New> News { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Account>()
            //     .Property(o => o.Role)
            //     .HasConversion<string>();
            // modelBuilder.Entity<Account>()
            //     .Property(o => o.EnumAccountStatus)
            //     .HasConversion<string>();
        }
    }
}

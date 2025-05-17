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
        public DbSet<PostPackage> PostPackages { get; set; }
        public DbSet<AccountService> AccountServices { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Invetor> Inventories { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<NewTag> NewTags { get; set; }
        public DbSet<OwnerProperty> OwnerProperties { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Provide> Providers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Support> Supports { get; set; }

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

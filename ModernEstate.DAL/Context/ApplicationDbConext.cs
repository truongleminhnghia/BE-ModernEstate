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
        public DbSet<AccountBuyService> AccountBuyServices { get; set; }
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

        private static readonly TimeZoneInfo _vnZone =
        TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        // Trả về DateTime ở múi VN
        private DateTime GetCurrentVnTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _vnZone);
        }

        public override int SaveChanges()
        {
            ApplyTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            ApplyTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyTimestamps()
        {
            DateTime now = GetCurrentVnTime();

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    entry.Entity.UpdatedAt = now;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

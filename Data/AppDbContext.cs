using CustomerManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security;

namespace CustomerManagement.Data
{


    public class AppDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //...
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Replace("AspNet", string.Empty));
                }
            }

            //config softdelete
            modelBuilder.Entity<Customer>().HasQueryFilter(u => u.IsDeleted == false);
            modelBuilder.Entity<CustomerAddress>().HasQueryFilter(u => u.IsDeleted == false);

            //config enum when store into sql server
            modelBuilder.Entity<Customer>().Property(q => q.Sex).HasConversion<string>().HasMaxLength(50);



        }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Ward> Wards { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresss { get; set; }
    }
}

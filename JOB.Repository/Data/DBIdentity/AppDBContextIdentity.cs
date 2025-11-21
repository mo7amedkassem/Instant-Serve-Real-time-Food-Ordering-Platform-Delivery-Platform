using Booking.Core.Entity;
using Job.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Data.DBIdentity
{
    public class AppDBContextIdentity : IdentityDbContext<AppUser>
    {
        public AppDBContextIdentity(DbContextOptions<AppDBContextIdentity> options) :
            base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

          // Configure the relationship between AppUser and Product
          builder.Entity<Product>()
         .HasOne(p => p.User)
         .WithMany(u => u.Products)
         .HasForeignKey(p => p.UserId)
         .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders {get; set;}
        public DbSet<SavedProduct> SavedProducts { get; set; }
    }
}

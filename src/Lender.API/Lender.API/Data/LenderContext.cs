using Lender.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lender.API.Data
{
    public class LenderContext : IdentityDbContext<AppUser>
    {
        public LenderContext(DbContextOptions<LenderContext> options) : base(options) { }

        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreationTime") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreationTime").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreationTime").IsModified = false;
                }
            }

            var success = await base.SaveChangesAsync() > 0;

            if (!success) throw new Exception("Problem saving changes");

            return success;
        }
    }
}

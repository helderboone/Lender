using Lender.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lender.API.Data
{
    public class LenderContext : IdentityDbContext<AppUser>
    {
        public LenderContext(DbContextOptions<LenderContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

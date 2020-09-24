using Lender.API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lender.API.Data.Seed
{
    public class Seed
    {
        public static async Task SeedData(LenderContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "joao",
                        Email = "joao@email.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "P@ssw0rd123");
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Friend> Friends { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}

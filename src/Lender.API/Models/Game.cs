using Lender.API.Models.Base;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class Game : Entity
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Url { get; set; }

        public string PublicId { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public void Update(string name, string gender)
        {
            Name = name;
            Gender = gender;
        }
    }
}

using Lender.API.Models.Base;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class Friend : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Url { get; set; }

        public string PublicId { get; set; }

        public AppUser User { get; set; }

        public string UserId { get; set; }

        public Address Address { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public void Update(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}

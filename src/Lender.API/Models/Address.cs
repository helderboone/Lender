using Lender.API.Models.Base;
using Lender.API.Models.Validators;

namespace Lender.API.Models
{
    public class Address : Entity
    {
        public Address(string number, string street, string neighborhood, string city)
        {
            Number = number;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            Validar();
        }

        protected Address() { }

        public string Number { get; private set; }

        public string Street { get; private set; }

        public string Neighborhood { get; private set; }

        public string City { get; private set; }

        public long FriendId { get; private set; }

        public Friend Friend { get; private set; }

        public void Update(string number, string street, string neighborhood, string city)
        {
            Number = number;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            Validar();
        }

        public override bool Validar()
        {
            return Validate(this, new AddressValidator());
        }
    }
}

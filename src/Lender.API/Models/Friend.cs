using Lender.API.Helper;
using Lender.API.Models.Base;
using Lender.API.Models.Validators;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class Friend : Entity
    {
        public Friend(string name, string email, string phone, Address address, AppUser user)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            User = user;
            _loans = new List<Loan>();
            Validar();
        }

        protected Friend()
        {
            _loans = new List<Loan>();
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string PhotoUrl { get; private set; }

        public string PhotoPublicId { get; private set; }

        public AppUser User { get; private set; }

        public string UserId { get; private set; }

        public Address Address { get; private set; }

        private readonly List<Loan> _loans;
        public IReadOnlyCollection<Loan> Loans => _loans;

        public void Update(string name, string email, string phone, Address address, PhotoUploadResult photo)
        {
            Name = name;
            Email = email;
            Phone = phone;
            PhotoUrl = photo?.Url;
            PhotoPublicId = photo?.PublicId;
            Address?.Update(address?.Number, address?.Street, address?.Neighborhood, address?.City);
            Validar();
        }

        public void AddPhoto(PhotoUploadResult photo)
        {
            PhotoUrl = photo?.Url;
            PhotoPublicId = photo?.PublicId;
            Validar();
        }

        public void AssociateUser(AppUser user)
        {
            User = user;
            Validar();
        }

        public void AddLoan(Loan loan)
        {
            if (loan == null) return;

            _loans.Add(loan);
            Validar();
        }

        public override bool Validar()
        {
            return Validate(this, new FriendValidator());
        }
    }
}

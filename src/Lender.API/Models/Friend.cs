using Lender.API.Helper;
using Lender.API.Models.Base;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class Friend : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhotoUrl { get; set; }

        public string PhotoPublicId { get; set; }

        public AppUser User { get; set; }

        public string UserId { get; set; }

        public Address Address { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public void Update(string name, string email, string phone, Address address, PhotoUploadResult photo)
        {
            Name = name;
            Email = email;
            Phone = phone;
            PhotoUrl = photo.Url;
            PhotoPublicId = photo.PublicId;
            Address?.Update(address.Number, address.Street, address.Neighborhood, address.City);
        }

        public void AddPhoto(PhotoUploadResult photo)
        {
            PhotoUrl = photo.Url;
            PhotoPublicId = photo.PublicId;
        }
    }
}

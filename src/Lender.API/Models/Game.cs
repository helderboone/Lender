using Lender.API.Helper;
using Lender.API.Models.Base;
using Lender.API.Models.Validators;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class Game : Entity
    {
        public Game(string name, string gender, AppUser user)
        {
            Name = name;
            Gender = gender;
            User = user;
            _loans = new List<Loan>();
            Validar();
        }

        protected Game()
        {
            _loans = new List<Loan>();
        }

        public string Name { get; private set; }

        public string Gender { get; private set; }

        public string PhotoUrl { get; private set; }

        public string PhotoPublicId { get; private set; }

        public string UserId { get; private set; }
        public AppUser User { get; private set; }

        private readonly List<Loan> _loans;
        public IReadOnlyCollection<Loan> Loans => _loans;

        public void Update(string name, string gender, PhotoUploadResult photo)
        {
            Name = name;
            Gender = gender;
            PhotoUrl = photo?.Url;
            PhotoPublicId = photo?.PublicId;
            Validar();
        }

        public void AddPhoto(PhotoUploadResult photo)
        {
            PhotoUrl = photo?.Url;
            PhotoPublicId = photo?.PublicId;
            Validar();
        }

        public void AddLoan(Loan loan)
        {
            if (loan == null) return;
            _loans.Add(loan);
            Validar();
        }

        public void AssociateUser(AppUser user)
        {
            User = user;
            Validar();
        }

        public override bool Validar()
        {
            return Validate(this, new GameValidator());
        }
    }
}

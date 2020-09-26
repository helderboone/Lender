using FluentValidation;
using Lender.API.Helper;
using Lender.API.Models.Base;
using System.Collections.Generic;

namespace Lender.API.Models
{
    public class Game : Entity
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string PhotoUrl { get; set; }

        public string PhotoPublicId { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public void Update(string name, string gender, PhotoUploadResult photo)
        {
            Name = name;
            Gender = gender;
            PhotoUrl = photo.Url;
            PhotoPublicId = photo.PublicId;
        }

        public void AddPhoto(PhotoUploadResult photo)
        {
            PhotoUrl = photo.Url;
            PhotoPublicId = photo.PublicId;
        }

        public bool Validar()
        {
            return Validate(this, new GameValidator());
        }
    }

    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(255)
                .WithMessage("Name can has 255 caracters");
        }
    }
}

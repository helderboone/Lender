using FluentValidation;

namespace Lender.API.Models.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(a => a.Number)
                .NotEmpty()
                .WithMessage("Number is required")
                .MaximumLength(50)
                .WithMessage("Number can has 50 caracters");

            RuleFor(a => a.Street)
                .NotEmpty()
                .WithMessage("Street is required")
                .MaximumLength(100)
                .WithMessage("Street can has 100 caracters");

            RuleFor(a => a.Neighborhood)
                .NotEmpty()
                .WithMessage("Neighborhood is required")
                .MaximumLength(100)
                .WithMessage("Neighborhood can has 100 caracters");

            RuleFor(a => a.City)
                .NotEmpty()
                .WithMessage("City is required")
                .MaximumLength(100)
                .WithMessage("City can has 100 caracters");
        }
    }
}

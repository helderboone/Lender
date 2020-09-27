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
                .WithMessage("The Number max length is 50 characters");

            RuleFor(a => a.Street)
                .NotEmpty()
                .WithMessage("Street is required")
                .MaximumLength(100)
                .WithMessage("The Street max length is 100 characters");

            RuleFor(a => a.Neighborhood)
                .NotEmpty()
                .WithMessage("Neighborhood is required")
                .MaximumLength(100)
                .WithMessage("The Neighborhood max length is 100 characters");

            RuleFor(a => a.City)
                .NotEmpty()
                .WithMessage("City is required")
                .MaximumLength(100)
                .WithMessage("The City max length is 100 characters");
        }
    }
}

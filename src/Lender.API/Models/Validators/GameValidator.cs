using FluentValidation;

namespace Lender.API.Models.Validators
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(255)
                .WithMessage("The name max length is 255 characters");

            RuleFor(a => a.Gender)
                .NotEmpty()
                .WithMessage("Gender is required")
                .MaximumLength(50)
                .WithMessage("The gender max length is 50 characters");

            RuleFor(a => a.User)
                .NotNull()
                .WithMessage("User is required");
        }
    }
}

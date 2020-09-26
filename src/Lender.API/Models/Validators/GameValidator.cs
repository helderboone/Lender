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
                .WithMessage("Name can has 255 caracters");

            RuleFor(a => a.Gender)
                .NotEmpty()
                .WithMessage("Gender is required")
                .MaximumLength(50)
                .WithMessage("Gender can has 50 caracters");

            RuleFor(a => a.User)
                .NotNull()
                .WithMessage("User is required");
        }
    }
}

using FluentValidation;

namespace Lender.API.Models.Validators
{
    public class LoanValidator : AbstractValidator<Loan>
    {
        public LoanValidator()
        {
            RuleFor(a => a.Game)
                .NotNull()
                .WithMessage("Game is required");

            RuleFor(a => a.Friend)
                .NotNull()
                .WithMessage("Friend is required");
        }
    }
}

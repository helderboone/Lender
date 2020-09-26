using FluentValidation;

namespace Lender.API.Models.Validators
{
    public class FriendValidator : AbstractValidator<Friend>
    {
        public FriendValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(255)
                .WithMessage("Name can has 255 caracters");


            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Name is required")
                .EmailAddress();

            RuleFor(a => a.Phone)
                .NotEmpty()
                .WithMessage("Phone is required")
                .MaximumLength(20)
                .WithMessage("Phone can has 255 caracters");

            RuleFor(a => a.Address)
                .NotNull()
                .WithMessage("Address is required");
        }
    }
}

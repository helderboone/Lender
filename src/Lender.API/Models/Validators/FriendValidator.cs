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
                .WithMessage("The name max length is 255 characters");


            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email should use the following format example@email.com");

            RuleFor(a => a.Phone)
                .NotEmpty()
                .WithMessage("Phone is required")
                .MaximumLength(20)
                .WithMessage("The phone max length is 255 characters");

            RuleFor(a => a.Address)
                .NotNull()
                .WithMessage("Address is required");
        }
    }
}

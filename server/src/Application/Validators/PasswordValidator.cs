using FluentValidation;

namespace Application.Validators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Must not be empty. ")
                .MinimumLength(ApplicationConstants.MinPasswordLength)
                .WithMessage($"Must contain at least {ApplicationConstants.MinPasswordLength} characters. ")
                .MaximumLength(ApplicationConstants.MaxPasswordLength)
                .WithMessage($"Must contain mac {ApplicationConstants.MaxPasswordLength} characters. ")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W])[\S].{0,}$")
                .WithMessage(
                    "Password must contain at least one uppercase character, one lowercase character, " +
                    "one number, one special character and must not contain any white character. ");
        }
    }
}
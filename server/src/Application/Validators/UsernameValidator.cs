using FluentValidation;

namespace Application.Validators
{
    public class UsernameValidator : AbstractValidator<string>
    {
        public UsernameValidator()
        {
            RuleFor(username => username)
                .NotNull()
                .WithMessage("Must not be empty")
                .MinimumLength(ApplicationConstants.MinUsernameLength)
                .WithMessage($" Must contain at least {ApplicationConstants.MinUsernameLength} characters.")
                .MaximumLength(ApplicationConstants.MaxUsernameLength)
                .WithMessage($" Must contain max {ApplicationConstants.MaxUsernameLength} characters.")
                .Matches(@"^[^\s\W]+$")
                .WithMessage(" Must contain only alphanumeric characters or underscore.");
        }
    }
}
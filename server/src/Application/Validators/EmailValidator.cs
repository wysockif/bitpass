using FluentValidation;

namespace Application.Validators
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(email => email)
                .EmailAddress()
                .WithMessage("Not valid email address. ")
                .MinimumLength(ApplicationConstants.MinEmailLength)
                .WithMessage($" Must contain at least {ApplicationConstants.MinEmailLength} characters. ")
                .MaximumLength(ApplicationConstants.MaxEmailLength)
                .WithMessage($" Must contain max {ApplicationConstants.MaxEmailLength} characters. ");
        }
    }
}
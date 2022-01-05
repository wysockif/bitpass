using System;
using FluentValidation;

namespace Application.Validators
{
    public class EmailVerificationTokenValidator : AbstractValidator<string>
    {
        public EmailVerificationTokenValidator()
        {
            RuleFor(emailVerificationToken => emailVerificationToken)
                .NotNull()
                .Must(token => Guid.TryParse(token, out _))
                .WithMessage("Not valid token");
        }
    }
}
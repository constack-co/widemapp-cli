using Constack.Widemapp.Common.Constants;
using FluentValidation;

namespace Constack.Widemapp.Application.Services.Account.Commands.ResetPassword
{
    public class ResetAccountPasswordCommandValidator : AbstractValidator<ResetAccountPasswordCommand>
    {
        public ResetAccountPasswordCommandValidator()
        {
            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Email"));
            RuleFor(x => x.Token).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Token"));

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Password")).DependentRules(() =>
            {
                RuleFor(x => x.Password).MinimumLength(ValidatorConditions.PasswordMinLength).WithMessage(ValidatorMessages.MinLength("Password")).DependentRules(() =>
                {
                    RuleFor(x => x.Password).Matches(ValidatorRegex.Password).WithMessage(ValidatorMessages.FormatNotMatch("Password"));
                });
            });
        }
    }
}

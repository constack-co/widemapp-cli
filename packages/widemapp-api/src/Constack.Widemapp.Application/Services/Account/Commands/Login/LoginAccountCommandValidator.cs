using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using FluentValidation;

namespace Constack.Widemapp.Application.Services.Account.Commands.Login
{
    public class LoginAccountCommandValidator : AbstractValidator<LoginAccountCommand>
    {
        private readonly IAutomationDbContext _context;
        public LoginAccountCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Password"));

            RuleFor(x => x.EmailOrUsername).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Username or Email"));
        }
    }
}

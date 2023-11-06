using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Users.Commands.Add
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IAutomationDbContext _context;
        public AddUserCommandValidator(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("FirstName"));

            RuleFor(x => x.LastName).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("LastName"));

            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Email")).DependentRules(() =>
            {
                RuleFor(x => x.Email).Matches(ValidatorRegex.Email).WithMessage(ValidatorMessages.FormatNotMatch("Email")).DependentRules(() =>
                {
                    RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
                    {
                        return !await _context.Users.AsNoTracking().AnyAsync(x => x.Email.ToLower() == email.ToLower(), cancellation);
                    }).WithMessage(x => ValidatorMessages.AlreadyExists(x.Email));
                });
            });

            RuleFor(x => x.UserName).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Username")).DependentRules(() =>
            {
                RuleFor(x => x.UserName).MinimumLength(ValidatorConditions.UserNameMinLength).WithMessage(ValidatorMessages.MinLength("Username", ValidatorConditions.UserNameMinLength)).DependentRules(() =>
                {
                    RuleFor(x => x.UserName).Matches(ValidatorRegex.UserName).WithMessage(ValidatorMessages.FormatNotMatch("Username")).DependentRules(() =>
                    {
                        RuleFor(x => x.UserName).MustAsync(async (username, cancellation) =>
                        {
                            return !await _context.Users.AsNoTracking().AnyAsync(x => x.UserName.ToLower() == username.ToLower(), cancellation);
                        }).WithMessage(x => ValidatorMessages.AlreadyExists(x.UserName));
                    });
                });
            });

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

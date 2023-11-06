using Constack.Widemapp.Application.Services.Account.Commands.ForgetPassword.Notification;
using Constack.Widemapp.Common.Constants;
using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Common.Helpers;
using Constack.Widemapp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Constack.Widemapp.Application.Services.Account.Commands.ForgetPassword
{
    public class ForgetAccountPasswordCommandHandler : IRequestHandler<ForgetAccountPasswordCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        public ForgetAccountPasswordCommandHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(ForgetAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            await RecaptchaHelper.ValidateRecaptcha();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new BadRequestException(ValidatorMessages.NotFound(request.Email));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var notification = new AccountForgetPasswordNotification
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                UserName = user.UserName
            };

            await Task.Factory.StartNew(() => _mediator.Send(notification, cancellationToken));

            return Unit.Value;
        }
    }
}

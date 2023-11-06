using Constack.Widemapp.Common.Constants;
using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Common.Extensions;
using Constack.Widemapp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Constack.Widemapp.Application.Services.Account.Commands.ResetPassword
{
    public class ResetAccountPasswordCommandHandler : IRequestHandler<ResetAccountPasswordCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        public ResetAccountPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<Unit> Handle(ResetAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new BadRequestException(ValidatorMessages.NotFound(request.Email));

            var isValidToken = await _userManager.ResetPasswordAsync(user, request.Token.DecodeBase64(Encoding.UTF8), request.Password);

            if (!isValidToken.Succeeded) throw new BadRequestException(ValidatorMessages.ExpiredToken);

            return Unit.Value;
        }
    }
}

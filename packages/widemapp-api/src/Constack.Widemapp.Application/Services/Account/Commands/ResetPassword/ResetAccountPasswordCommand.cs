using MediatR;

namespace Constack.Widemapp.Application.Services.Account.Commands.ResetPassword
{
    public class ResetAccountPasswordCommand : IRequest<Unit>
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

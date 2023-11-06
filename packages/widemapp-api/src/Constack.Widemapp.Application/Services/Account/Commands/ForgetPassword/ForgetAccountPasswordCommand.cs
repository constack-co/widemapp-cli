using MediatR;

namespace Constack.Widemapp.Application.Services.Account.Commands.ForgetPassword
{
    public class ForgetAccountPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}

using MediatR;

namespace Constack.Widemapp.Application.Services.Account.Commands.Login
{
    public class LoginAccountCommand : IRequest<LoginAccountModel>
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}

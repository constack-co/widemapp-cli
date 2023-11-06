using MediatR;

namespace Constack.Widemapp.Application.Services.Account.Commands.ForgetPassword.Notification
{
    public class AccountForgetPasswordNotification : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}

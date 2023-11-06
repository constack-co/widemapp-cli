using Constack.Widemapp.Common.Extensions;
using Constack.Widemapp.Common.Helpers;
using Constack.Widemapp.Common.ServiceContexts.Interfaces;
using MediatR;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Constack.Widemapp.Application.Services.Account.Commands.ForgetPassword.Notification
{
    public class AccountForgetPasswordNotificationHandler : IRequestHandler<AccountForgetPasswordNotification, Unit>
    {
        private readonly IMailContext _mailContext;
        public AccountForgetPasswordNotificationHandler(IMailContext mailContext)
        {
            _mailContext = mailContext ?? throw new ArgumentNullException(nameof(mailContext));
        }

        public async Task<Unit> Handle(AccountForgetPasswordNotification request, CancellationToken cancellationToken)
        {
            var mail = await _mailContext.BuildClientAsync();

            var file = await EmbededHelper.ReadEmbededFileAsync(Assembly.GetExecutingAssembly(), "ResetPasswordTemplate.html");

            var mailMessage = new MailMessage
            {
                From = new MailAddress("info@constack.co"),
                Subject = "Reset your password",
                Body = file.Replace("##FIRSTNAME##", request.FirstName)
                           .Replace("##LASTNAME##", request.LastName)
                           .Replace("##HREF##", $"{_mailContext.ClientUrl}/reset-password?token={request.Token.EncodeBase64(Encoding.UTF8)}&email={request.Email.EncodeBase64(Encoding.UTF8)}"),
                IsBodyHtml = true,
            };

            mailMessage.To.Add(request.Email);

            await mail.SendMailAsync(mailMessage, cancellationToken);

            return Unit.Value;
        }
    }
}

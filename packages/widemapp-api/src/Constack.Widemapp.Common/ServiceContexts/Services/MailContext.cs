using Constack.Widemapp.Common.ServiceContexts.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Constack.Widemapp.Common.ServiceContexts.Services
{
    public class MailContext : IMailContext
    {
        private readonly EmailCredentialContext _credential;
        public string AppUrl { get; private set; }
        public string ClientUrl { get; private set; }

        public MailContext(EmailCredentialContext credential, string appUrl, string clientUrl)
        {
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));

            AppUrl = appUrl;

            ClientUrl = clientUrl;
        }

        public async Task<SmtpClient> BuildClientAsync()
        {
            return await Task.FromResult(new SmtpClient
            {
                Port = _credential.Port,
                Credentials = new NetworkCredential(_credential.Email, _credential.Password),
                EnableSsl = _credential.EnableSsl,
                Host = _credential.Host
            });
        }

        public class EmailCredentialContext
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public int Port { get; set; }
            public bool EnableSsl { get; set; }
            public string Host { get; set; }
            public string ClientUrl { get; set; }
        }
    }
}

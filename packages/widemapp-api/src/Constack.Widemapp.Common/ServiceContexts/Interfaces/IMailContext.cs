using System.Net.Mail;

namespace Constack.Widemapp.Common.ServiceContexts.Interfaces
{
    public interface IMailContext
    {
        Task<SmtpClient> BuildClientAsync();
        public string AppUrl { get; }
        public string ClientUrl { get; }
    }
}

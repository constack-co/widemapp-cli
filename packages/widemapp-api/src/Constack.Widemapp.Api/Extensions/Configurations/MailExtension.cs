using Constack.Widemapp.Common.ServiceContexts.Interfaces;
using Constack.Widemapp.Common.ServiceContexts.Services;

namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class MailExtension
    {
        public static void AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMailContext, MailContext>(x =>
                new MailContext(new MailContext.EmailCredentialContext
                {
                    Email = configuration["Email:CredentialsEmail"],
                    Password = configuration["Email:CredentialsPasword"],
                    EnableSsl = bool.Parse(configuration["Email:EnableSsl"]),
                    Port = int.Parse(configuration["Email:Port"]),
                    Host = configuration["Email:Host"],
                }, 
                configuration["ApplicationDefaults:AppUrl"],
                configuration["ApplicationDefaults:ClientUrl"]
            ));
        }
    }
}

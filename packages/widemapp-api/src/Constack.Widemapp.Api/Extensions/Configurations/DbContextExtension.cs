using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class DbContextExtension
    {
        public static void AddRegisteredDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AutomationDatabase");

            services.AddDbContext<AutomationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(
                  maxRetryCount: 5,
                  maxRetryDelay: TimeSpan.FromSeconds(30),
                  errorNumbersToAdd: null);
            }));

            services.AddScoped<IAutomationDbContext, AutomationDbContext>();
        }
    }
}

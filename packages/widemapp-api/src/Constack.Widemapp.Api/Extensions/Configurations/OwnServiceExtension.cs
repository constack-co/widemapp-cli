using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Persistence;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class OwnServiceExtension
    {
        public static void AddRegistredServices(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAutomationDbContext, AutomationDbContext>();
            services.AddHttpContextAccessor();
        }
    }
}

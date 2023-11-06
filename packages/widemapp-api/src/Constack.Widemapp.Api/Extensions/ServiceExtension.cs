using Constack.Widemapp.Api.Extensions.Configurations;
using Constack.Widemapp.Api.Filters;

namespace Constack.Widemapp.Api.Extensions
{
    public static class ServiceExtension
    {
        private const string CorsPolicy = "_corsPolicy";
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(option => option.Filters.Add<ExceptionFilter>());
            services.AddRegisteredDbContext(configuration);
            services.AddRegisteredCors(CorsPolicy);
            services.AddRegisteredAuthentication(configuration);
            services.RegisterSwagger();
            services.AddRegisteredMediator();
            services.AddRegistredServices();
            services.AddHttpContextAccessor();
            services.AddMailServices(configuration);
            return services;
        }

        public static void UseServices(this IMvcBuilder builder)
        {
            builder.UseValidations();
        }

        public static IApplicationBuilder AddServices(this WebApplication app, IConfiguration configuration)
        {
            app.UseCors(CorsPolicy);
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseRegisteredSwagger();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHelpers();
            //app.UseAuthentication();
            //app.UseAuthorization();
            return app;
        }
    }
}

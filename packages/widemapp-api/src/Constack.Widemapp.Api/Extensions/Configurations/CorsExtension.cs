namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class CorsExtension
    {
        public static void AddRegisteredCors(this IServiceCollection services, string corsPolicy)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy,
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}

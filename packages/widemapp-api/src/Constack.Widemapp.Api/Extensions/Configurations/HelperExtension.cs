using Constack.Widemapp.Common.Helpers;

namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class HelperExtension
    {
        public static void UseHelpers(this IApplicationBuilder app)
        {
            AuthHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            RecaptchaHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}

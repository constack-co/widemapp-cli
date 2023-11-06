using Constack.Widemapp.Application.Infrastructure.Mediator;
using Constack.Widemapp.Application.Services.Account.Commands.Login;
using MediatR;

namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class MediatorExtension
    {
        public static void AddRegisteredMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginAccountCommand).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
        }
    }
}

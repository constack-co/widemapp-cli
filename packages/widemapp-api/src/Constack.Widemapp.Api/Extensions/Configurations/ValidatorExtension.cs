using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Application.Services.Entities.Commands.AddRelation;
using FluentValidation.AspNetCore;

namespace Constack.Widemapp.Api.Extensions.Configurations
{
    public static class ValidatorExtension
    {
        public static void UseValidations(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining(typeof(IAutomationDbContext));
                fv.DisableDataAnnotationsValidation = true;
            });
        }
    }
}

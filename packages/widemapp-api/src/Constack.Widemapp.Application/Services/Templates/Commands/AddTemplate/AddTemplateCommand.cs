using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddTemplate
{
    public class AddTemplateCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }

        public Template AddTemplate(Guid microserviceId)
        {
            return new Template()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                MicroserviceId = microserviceId
            };
        }
    }
}

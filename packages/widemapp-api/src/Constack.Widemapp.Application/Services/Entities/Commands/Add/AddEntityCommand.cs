using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.Add
{
    public class AddEntityCommand : IRequest<AddEntityResponseModel>
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }

        public Entity AddEntity(Guid microserviceId)
        {
            return new Entity
            {
                Name = Name,
                MicroserviceId = microserviceId
            };
        }
    }
}

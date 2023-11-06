using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.RemoveRelation
{
    public class DeleteEntityRelationCommand : IRequest<Unit>
    {
        public Guid EntityFromId { get; set; }
        public Guid EntityToId { get; set; }
        public Guid? PropertyFromId { get; set; }
        public Guid PropertyToId { get; set; }
    }
}

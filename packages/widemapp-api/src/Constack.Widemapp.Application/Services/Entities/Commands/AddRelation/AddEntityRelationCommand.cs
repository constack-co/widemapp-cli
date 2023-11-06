using Constack.Widemapp.Domain.Entities;
using Constack.Widemapp.Domain.Enums;
using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.AddRelation
{
    public class AddEntityRelationCommand : IRequest<Unit>
    {
        public Guid EntityFromId { get; set; }
        public Guid EntityToId { get; set; }
        public Guid PropertyFromId { get; set; }
        public Guid PropertyToId { get; set; }

        public EntityRelation AddEntityRelation()
        {
            return new EntityRelation
            {
                EntityFromId = EntityFromId,
                EntityToId = EntityToId,
                PropertyFromId = PropertyFromId,
                PropertyToId = PropertyToId,
                RelationTypeId = RelationTypeEnum.OneToMany
            };
        }
    }
}

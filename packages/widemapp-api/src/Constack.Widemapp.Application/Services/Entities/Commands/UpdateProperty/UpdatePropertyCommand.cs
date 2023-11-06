using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.UpdateProperty
{
    public class UpdatePropertyCommand : IRequest<Unit>
    {
        public Guid PropertyId { get; set; }
        public string AttributeToUpdate { get;set;}
        public string? Name { get; set; }
        public bool? IsNullable { get; set; }
        public Guid? PropertyTypeId { get; set; }
    }
}

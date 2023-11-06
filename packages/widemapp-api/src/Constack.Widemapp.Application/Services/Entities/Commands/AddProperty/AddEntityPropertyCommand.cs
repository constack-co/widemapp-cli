using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.AddProperty
{
    public class AddEntityPropertyCommand : IRequest<AddEntityPropertyResponseModel>
    {
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid EntityId { get; set; }

        public async Task<Property> AddProperty(IAutomationDbContext context)
        {
            var property = new Property
            {
                Id = Guid.NewGuid(),
                Name = Name,
                IsNullable = IsNullable,
                PropertyTypeId = PropertyTypeId
            };

            await context.Properties.AddAsync(property);
            await context.EntityProperties.AddAsync(new EntityProperty()
            {
                Id = Guid.NewGuid(),
                EntityId = EntityId,
                PropertyId = property.Id
            });

            return property;
        }
    }
}

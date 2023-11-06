using Constack.Widemapp.Application.Interfaces;
using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.AddProperty
{
    public class AddEntityPropertyCommandHandler : IRequestHandler<AddEntityPropertyCommand, AddEntityPropertyResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public AddEntityPropertyCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<AddEntityPropertyResponseModel> Handle(AddEntityPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await request.AddProperty(_context);
            await _context.SaveChangesAsync();

            var entityProperty = _context.EntityProperties.FirstOrDefault(x => x.PropertyId == property.Id);
            return new AddEntityPropertyResponseModel
            {
                Id = property.Id,
                EntityProperty = new EntityPropertyModel
                {
                    Id = entityProperty.Id,
                }
            };
        }
    }
}

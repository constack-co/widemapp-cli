using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Entities.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public UpdatePropertyCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == request.PropertyId);
            if (property == null) throw new NotFoundException("Property");
            if (request.AttributeToUpdate == "Name")
            {
                property.Name = request.Name;
            }
            else if (request.AttributeToUpdate == "IsNullable")
            {
                property.IsNullable = request.IsNullable ?? false;
            }
            else if(request.AttributeToUpdate == "PropertyTypeId")
            {
                if (request.PropertyTypeId != null)
                {
                    property.PropertyTypeId = request.PropertyTypeId ?? Guid.Empty;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

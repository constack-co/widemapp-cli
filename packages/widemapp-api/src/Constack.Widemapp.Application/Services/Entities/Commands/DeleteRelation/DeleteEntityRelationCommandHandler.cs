using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Application.Services.Entities.Commands.RemoveRelation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Entities.Commands.DeleteRelation
{
    public class DeleteEntityRelationCommandHandler : IRequestHandler<DeleteEntityRelationCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public DeleteEntityRelationCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(DeleteEntityRelationCommand request, CancellationToken cancellationToken)
        {
            var entityRelationQuery = _context.EntityRelations
                .Where(x => x.EntityToId == request.EntityToId)
                .Where(x => x.PropertyToId == request.PropertyToId)
            ;

            if (request.PropertyFromId != null)
            {
                entityRelationQuery.Where(x => x.PropertyFromId == request.PropertyFromId);
            }

            var entityRealtion = await entityRelationQuery.FirstOrDefaultAsync(x => x.EntityFromId == request.EntityFromId);

            if (entityRealtion != null)
                _context.EntityRelations.Remove(entityRealtion);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

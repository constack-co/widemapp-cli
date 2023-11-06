using Constack.Widemapp.Application.Interfaces;
using MediatR;

namespace Constack.Widemapp.Application.Services.Entities.Commands.AddRelation
{
    public class AddEntityRelationCommandHandler : IRequestHandler<AddEntityRelationCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public AddEntityRelationCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Unit> Handle(AddEntityRelationCommand request, CancellationToken cancellationToken)
        {
            await _context.EntityRelations.AddAsync(request.AddEntityRelation(), cancellationToken);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

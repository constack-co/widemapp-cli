using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Commands.RenameGroup
{
    public class RenameGroupCommandHandler : IRequestHandler<RenameGroupCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public RenameGroupCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(RenameGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (group != null)
            {
                group.Name = request.Name;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

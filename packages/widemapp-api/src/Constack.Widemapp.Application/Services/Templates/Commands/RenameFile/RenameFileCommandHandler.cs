using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Commands.RenameFile
{
    public class RenameFileCommandHandler : IRequestHandler<RenameFileCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public RenameFileCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(RenameFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (file != null)
            {
                file.Name = request.Name;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

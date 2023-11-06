using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Commands.DeleteFileOrGroup
{
    public class DeleteFileOrGroupCommandHandler : IRequestHandler<DeleteFileOrGroupCommand, Unit>
    {
        private readonly IAutomationDbContext _context;
        public DeleteFileOrGroupCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(DeleteFileOrGroupCommand request, CancellationToken cancellationToken)
        {
            if (request.Type == "file")
            {
                var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == request.Id);
                _context.Files.Remove(file);
            }
            else if (request.Type == "group" || request.Type == "folder")
            {
                var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == request.Id);
                _context.Groups.Remove(group);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

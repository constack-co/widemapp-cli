using Constack.Widemapp.Application.Interfaces;
using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddFile
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, Guid>
    {
        private readonly IAutomationDbContext _context;
        public AddFileCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.AddFile();
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();
            return file.Id;
        }
    }
}

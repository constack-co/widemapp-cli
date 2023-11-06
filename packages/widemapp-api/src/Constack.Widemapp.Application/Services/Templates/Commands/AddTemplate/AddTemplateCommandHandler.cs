using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddTemplate
{
    public class AddTemplateCommandHandler : IRequestHandler<AddTemplateCommand, Guid>
    {
        private readonly IAutomationDbContext _context;
        public AddTemplateCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
        {
            var microservice = await _context.Microservices.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId);

            var template = request.AddTemplate(microservice.Id);
            await _context.Templates.AddAsync(template);
            await _context.SaveChangesAsync(cancellationToken);
            return template.Id;
        }
    }
}

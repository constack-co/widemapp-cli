using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Helpers;
using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Projects.Commands.Add
{
    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, Guid>
    {
        private readonly IAutomationDbContext _context;
        public AddProjectCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var userId = AuthHelper.AuthId;

            var project = request.AddProject(userId);
            var microservice = new Microservice
            {
                Id = Guid.NewGuid(),
                Name = "default",
                ProjectId = project.Id,
            };

            await _context.Projects.AddAsync(project);
            await _context.Microservices.AddAsync(microservice);
            await _context.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
    }
}

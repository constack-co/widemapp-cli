using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Entities.Commands.Add
{
    public class AddEntityCommandHandler : IRequestHandler<AddEntityCommand, AddEntityResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public AddEntityCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<AddEntityResponseModel> Handle(AddEntityCommand request, CancellationToken cancellationToken)
        {
            var microservice = await _context.Microservices.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId);
            var entity = request.AddEntity(microservice.Id);
            await _context.Entities.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return new AddEntityResponseModel
            {
                Id = entity.Id
            };
        }
    }
}

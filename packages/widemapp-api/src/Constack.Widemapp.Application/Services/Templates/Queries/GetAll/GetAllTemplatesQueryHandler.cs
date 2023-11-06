using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetAll
{
    public class GetAllTemplatesQueryHandler : IRequestHandler<GetAllTemplatesQuery, IList<GetAllTemplatesResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetAllTemplatesQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetAllTemplatesResponseModel>> Handle(GetAllTemplatesQuery request, CancellationToken cancellationToken)
        {
            var microservice = await _context.Microservices.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId);

            return await _context.Templates
                .AsNoTracking()
                .Where(x => x.MicroserviceId == microservice.Id)
                .Select(x => new GetAllTemplatesResponseModel {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}

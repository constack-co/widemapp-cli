using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Projects.Queries.Get
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IList<GetProjectsResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetProjectsQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetProjectsResponseModel>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Projects
                .AsNoTracking()
                .Where(x => x.UserId == AuthHelper.AuthId)
                .Select(x => new GetProjectsResponseModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}

using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Groups.Queries.GetByTemplateId
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IList<GetGroupsResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetGroupsQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetGroupsResponseModel>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Groups
                .AsNoTracking()
                .Select(item => new GetGroupsResponseModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Type = item.Type,
                    TemplateId = item.TemplateId
                })
                .Where(x => x.TemplateId == request.TemplateId)
                .Where(x => x.Type == "group")
                .ToListAsync();
        }
    }
}

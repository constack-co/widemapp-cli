using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Plans.Queries.Get
{
    public class GetPlansQueryHandler : IRequestHandler<GetPlansQuery, IList<GetPlansResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetPlansQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetPlansResponseModel>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
        {
            return await _context.Plans
                .AsNoTracking()
                .Where(x => x.UserId == AuthHelper.AuthId)
                .Select(item => new GetPlansResponseModel
                {
                    Id = item.Id,
                    Name = item.Name
                })
                .ToListAsync();
        }
    }
}

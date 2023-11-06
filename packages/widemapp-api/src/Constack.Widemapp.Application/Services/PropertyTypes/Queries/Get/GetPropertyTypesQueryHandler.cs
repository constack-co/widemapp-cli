using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.PropertyTypes.Queries.Get
{
    public class GetPropertyTypesQueryHandler : IRequestHandler<GetPropertyTypesQuery, IList<GetPropertyTypesResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetPropertyTypesQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetPropertyTypesResponseModel>> Handle(GetPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.PropertyTypes
                .AsNoTracking()
                .Select(x => new GetPropertyTypesResponseModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
    }
}

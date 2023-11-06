using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Plans.Queries.GetGenerationTypes
{
    public class GetGenerationTypesQueryHandler : IRequestHandler<GetGenerationTypesQuery, IList<GetGenerationTypesResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetGenerationTypesQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetGenerationTypesResponseModel>> Handle(GetGenerationTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.GenerationTypes
                .AsNoTracking()
                .Select(item => new GetGenerationTypesResponseModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Value = item.Value
                })
                .ToListAsync();
        }
    }
}

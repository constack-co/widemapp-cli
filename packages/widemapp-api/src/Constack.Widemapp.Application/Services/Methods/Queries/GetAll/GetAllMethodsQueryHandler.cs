using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Methods.Queries.GetAll
{
    public class GetAllMethodsQueryHandler : IRequestHandler<GetAllMethodsQuery, IList<GetAllMethodsResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetAllMethodsQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetAllMethodsResponseModel>> Handle(GetAllMethodsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Methods
                .AsNoTracking()
                .Select(item => new GetAllMethodsResponseModel
                {
                    Id = item.Id,
                    Name = item.Name
                })
                .ToListAsync();
        }
    }
}

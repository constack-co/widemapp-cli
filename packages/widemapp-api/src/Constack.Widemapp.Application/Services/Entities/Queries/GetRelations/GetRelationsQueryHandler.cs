using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Entities.Queries.GetRelations
{
    public class GetRelationsQueryHandler : IRequestHandler<GetRelationsQuery, IList<GetRelationsResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetRelationsQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetRelationsResponseModel>> Handle(GetRelationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.EntityRelations
                .AsNoTracking()
                .Select(relation => new GetRelationsResponseModel
                {
                    From = relation.EntityFromId,
                    To = relation.EntityToId,
                    FromPort = $"o{relation.PropertyFromId}",
                    ToPort = $"i{relation.PropertyToId}",
                })
                .ToListAsync();
        }
    }
}

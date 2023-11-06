using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetGroup
{
    public class GetFolderQueryHandler : IRequestHandler<GetFolderQuery, GetFolderResponseModel>
    {
        private readonly IAutomationDbContext _context;
        public GetFolderQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<GetFolderResponseModel> Handle(GetFolderQuery request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups
                .AsNoTracking()
                .Select(groupItem => new GetFolderResponseModel
                {
                    Id = groupItem.Id,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (group == null) throw new NotFoundException($"{request.Id} not found");
            return group;
        }
    }
}

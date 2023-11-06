using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Groups.Queries.GetByName
{
    public class GetByNameCommandHandler : IRequestHandler<GetByNameCommand, IList<GetByNameCommandResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetByNameCommandHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetByNameCommandResponseModel>> Handle(GetByNameCommand request, CancellationToken cancellationToken)
        {
            return await _context.Groups
                .AsNoTracking()
                .Where(x => x.TemplateId == request.TemplateId)
                .Where(x => x.Type == "group")
                .Where(x => x.Name == request.GroupName)
                .Select(group => new GetByNameCommandResponseModel
                {
                    Id = group.Id,
                    GenerationType = new GenerationTypeModel
                    {
                        Id = group.GenerationTypeId,
                        Value = group.GenerationType.Value,
                    },
                })
                .ToListAsync();
        }
    }
}

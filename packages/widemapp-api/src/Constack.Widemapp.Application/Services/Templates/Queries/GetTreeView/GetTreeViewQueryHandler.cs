using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetTreeView
{
    public class GetTreeViewQueryHandler : IRequestHandler<GetTreeViewQuery, IList<GetTreeViewResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetTreeViewQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetTreeViewResponseModel>> Handle(GetTreeViewQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context
                .Groups
                .AsNoTracking()
                .Include(x => x.Files)
                .Include(x => x.Groups)
                .Select(group => new GetTreeViewResponseModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Type = group.Type,
                    GenerationTypeId = group.GenerationTypeId,
                    GroupId = group.GroupId,
                    TemplateId = group.TemplateId,
                    Children = group.Files.Select(file => new GetTreeViewResponseModel
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Type = "file",
                        TemplateId= file.TemplateId,
                        GroupId = file.GroupId,
                    }).ToList()
                })
                .ToListAsync();

            foreach (var group in groups)
            {
                if (group != null) {
                    GetTreeViewResponseModel foundGroup = null;
                    var parentNode = GetParentNode(foundGroup, groups, group);
                    if (parentNode != null) {
                        parentNode.Children.Add(group);            
                    }
                }
            }

            foreach (var group in groups)
            {
                if (group.GroupId != null)
                {
                    var findIndex = groups.FindIndex(x => x.Id == group.Id);
                    groups = groups.Where((source, index) => index != findIndex).ToList();
                }
            }

            return groups.Where(x => x.TemplateId == request.TemplateId).ToList();
        }

        private GetTreeViewResponseModel GetParentNode(GetTreeViewResponseModel foundGroup, IList<GetTreeViewResponseModel> groups, GetTreeViewResponseModel group)
        {
            foreach (var item in groups)
            {
                if (item.Id == group.GroupId)
                {
                    foundGroup = item;
                    break;
                }
                else if (item.Children.Count > 0)
                {
                    GetParentNode(foundGroup, item.Children, group);
                }
            }

            return foundGroup;
        }
    }
}

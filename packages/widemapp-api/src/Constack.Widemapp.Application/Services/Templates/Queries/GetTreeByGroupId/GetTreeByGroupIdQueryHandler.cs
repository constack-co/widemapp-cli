using Constack.Widemapp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetTreeByGroupId
{
    public class GetTreeByGroupIdQueryHandler : IRequestHandler<GetTreeByGroupIdQuery, IList<GetTreeByGroupIdResponseModel>>
    {
        private readonly IAutomationDbContext _context;
        public GetTreeByGroupIdQueryHandler(IAutomationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<GetTreeByGroupIdResponseModel>> Handle(GetTreeByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context
                .Groups
                .AsNoTracking()
                .Include(x => x.Files)
                .ThenInclude(x => x.FileEdits)
                .Include(x => x.Groups)
                .Select(group => new GetTreeByGroupIdResponseModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Type = group.Type,
                    GenerationTypeId = group.GenerationTypeId,
                    GroupId = group.GroupId,
                    TemplateId = group.TemplateId,
                    Children = group.Files.Select(file => new GetTreeByGroupIdResponseModel
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Type = "file",
                        ContentAdd = file.ContentAdd,
                        FileEdits = file.FileEdits.Select(fileEdit => new FileEditModel
                        {
                            Id = fileEdit.Id,
                            Placeholder = fileEdit.Placeholder,
                            Content = fileEdit.Content,
                            FileId = fileEdit.FileId
                        }).ToList(),
                        Action = file.Action,
                        TemplateId = file.TemplateId,
                        GroupId = file.GroupId,
                    }).ToList()
                })
                .ToListAsync();

            foreach (var group in groups)
            {
                if (group != null)
                {
                    GetTreeByGroupIdResponseModel foundGroup = null;
                    var parentNode = GetParentNode(foundGroup, groups, group);
                    if (parentNode != null)
                    {
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

            return groups.Where(x => x.Id == request.GroupId).ToList() ?? new List<GetTreeByGroupIdResponseModel>();
        }

        private GetTreeByGroupIdResponseModel GetParentNode(GetTreeByGroupIdResponseModel foundGroup, IList<GetTreeByGroupIdResponseModel> groups, GetTreeByGroupIdResponseModel group)
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

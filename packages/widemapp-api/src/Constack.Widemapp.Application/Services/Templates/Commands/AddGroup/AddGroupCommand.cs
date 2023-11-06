using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddGroup
{
    public class AddGroupCommand : IRequest<AddGroupResponseModel>
    {
        public string Name { get; set; }
        public string Type { get; set; } = "group";
        public Guid? GenerationTypeId { get; set; } = null;
        public Guid TemplateId { get; set; }
        public Guid? GroupId { get; set; } = null;

        public Group AddGroup()
        {
            return new Group()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                TemplateId = TemplateId,
                GenerationTypeId = GenerationTypeId,
                Type = Type == "group" ? "group" : "folder",
                GroupId = GroupId
            };
        }
    }
}

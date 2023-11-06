using Constack.Widemapp.Domain.Enums;
using MediatR;
using File = Constack.Widemapp.Domain.Entities.File;

namespace Constack.Widemapp.Application.Services.Templates.Commands.AddFile
{
    public class AddFileCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid GroupId { get; set; }
        public Guid TemplateId { get; set; }
        public string Action { get; set; }

        public File AddFile()
        {
            return new File()
            {
               Id = Guid.NewGuid(),
               Name = Name,
               Path = "",
               Action = Action == FileEnum.EDIT ? FileEnum.EDIT : FileEnum.ADD_EDIT,
               Placeholder = "",
               TemplateId = TemplateId,
               GroupId = GroupId
            };
        }
    }
}

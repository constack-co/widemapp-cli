using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetTreeByGroupId
{
    public class GetTreeByGroupIdResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? ContentAdd { get; set; }
        public IList<FileEditModel>? FileEdits { get; set; }
        public string? Action { get; set; }
        public Guid? GenerationTypeId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid TemplateId { get; set; }
        public IList<GetTreeByGroupIdResponseModel> Children { get; set; } = new List<GetTreeByGroupIdResponseModel>();
    }

    public class FileEditModel
    {
        public Guid Id { get; set; }
        public string Placeholder { get; set; }
        public string Content { get; set; }
        public Guid FileId { get; set; }
    }
}

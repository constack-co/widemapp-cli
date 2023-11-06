using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Templates.Queries.GetTreeView
{
    public class GetTreeViewResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid? GenerationTypeId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid TemplateId { get; set; }
        public IList<GetTreeViewResponseModel> Children { get; set; } = new List<GetTreeViewResponseModel>();
    }
}

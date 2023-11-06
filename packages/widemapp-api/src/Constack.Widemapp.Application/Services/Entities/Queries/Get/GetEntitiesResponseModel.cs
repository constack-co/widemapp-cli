using Constack.Widemapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Entities.Queries.Get
{
    public class GetEntitiesResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<PropertyModel> Properties { get; set; }
        public IList<string> Inputs { get; set; }
        public IList<string> Outputs { get; set; }
    }

    public class PropertyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PropertyTypeModel Type { get; set; }
        public bool IsNullable { get; set; }
    }

    public class PropertyTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

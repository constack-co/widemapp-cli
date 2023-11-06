using Constack.Widemapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Application.Services.Entities.Commands.AddProperty
{
    public class AddEntityPropertyResponseModel
    {
        public Guid Id { get; set; }
        public EntityPropertyModel EntityProperty { get; set; }
    }

    public class EntityPropertyModel
    {
        public Guid Id { get; set; }
    }
}

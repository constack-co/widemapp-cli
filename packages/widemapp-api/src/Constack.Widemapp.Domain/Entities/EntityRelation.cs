using Constack.Widemapp.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constack.Widemapp.Domain.Entities
{
    public class EntityRelation : BaseEntity<Guid>
    {
        public Guid EntityFromId { get; set; }
        public Guid EntityToId { get; set; }
        public Guid PropertyFromId { get; set; }
        public Guid PropertyToId { get; set; }
        public Guid RelationTypeId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual Property Property { get; set; }
        public virtual RelationType RelationType { get; set; }
    }
}

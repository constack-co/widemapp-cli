using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Method : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<Api> Apis { get; set; }
    }
}

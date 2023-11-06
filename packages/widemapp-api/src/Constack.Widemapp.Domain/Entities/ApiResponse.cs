using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class ApiResponse : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? IsNullable { get; set; }
        public Guid ApiId { get; set; }

        public virtual Api Api { get; set; }
    }
}

using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class FileEdit : BaseEntity<Guid>
    {
        public string Placeholder { get; set; }
        public string Content { get; set; }
        public Guid FileId { get; set; }

        public virtual File File { get; set; }
    }
}

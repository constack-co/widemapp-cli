using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class File : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string? ContentAdd { get; set; }
        public string Action { get; set; }
        public string Placeholder { get; set; }
        public Guid TemplateId { get; set; }
        public Guid GroupId { get; set; }

        public virtual Template Template { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<FileEdit> FileEdits { get; set; }
    }
}

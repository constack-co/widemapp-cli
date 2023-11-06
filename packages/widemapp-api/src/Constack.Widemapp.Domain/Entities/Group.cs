using Constack.Widemapp.Domain.Entities.Base;

namespace Constack.Widemapp.Domain.Entities
{
    public class Group : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Guid TemplateId { get; set; }
        public string Type { get; set; }
        public Guid? GenerationTypeId { get; set; }
        public Guid? GroupId { get; set; }

        public virtual Group GroupOwn { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual Template Template { get; set; }
        public virtual GenerationType GenerationType { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}

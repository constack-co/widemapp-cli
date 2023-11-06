namespace Constack.Widemapp.Domain.Entities.Base
{
    public class BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        public Tkey Id { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

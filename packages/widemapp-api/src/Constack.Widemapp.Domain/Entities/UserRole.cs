using Microsoft.AspNetCore.Identity;

namespace Constack.Widemapp.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? Discriminator { get; set; }
    }
}

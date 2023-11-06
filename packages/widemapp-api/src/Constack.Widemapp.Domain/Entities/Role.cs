using Microsoft.AspNetCore.Identity;

namespace Constack.Widemapp.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Constack.Widemapp.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Projects = new HashSet<Project>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}

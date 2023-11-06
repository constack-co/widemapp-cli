using Microsoft.AspNetCore.Identity;

namespace Constack.Widemapp.Domain.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public string Discriminator { get; set; }
    }
}

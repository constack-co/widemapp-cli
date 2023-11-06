using Constack.Widemapp.Domain.Entities;
using MediatR;

namespace Constack.Widemapp.Application.Services.Users.Commands.Add
{
    public class AddUserCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public User AddUser()
        {
            return new User
            {
                UserName = UserName,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }
    }
}

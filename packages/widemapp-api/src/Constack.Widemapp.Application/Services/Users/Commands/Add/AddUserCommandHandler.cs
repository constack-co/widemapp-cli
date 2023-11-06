using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Common.Helpers;
using Constack.Widemapp.Domain.Entities;
using Constack.Widemapp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Constack.Widemapp.Application.Services.Users.Commands.Add
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AddUserCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await RecaptchaHelper.ValidateRecaptcha();

            var role = await _roleManager.FindByNameAsync(RoleEnum.Administrator);

            if (role == null) throw new NotFoundException("Role");

            await _userManager.CreateAsync(request.AddUser(), request.Password);

            var user = await _userManager.FindByEmailAsync(request.Email);

            await _userManager.AddToRoleAsync(user, role.Name);

            return Unit.Value;
        }
    }
}

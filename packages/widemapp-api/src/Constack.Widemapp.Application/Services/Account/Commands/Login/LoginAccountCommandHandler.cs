using Constack.Widemapp.Application.Interfaces;
using Constack.Widemapp.Common.Constants;
using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Domain.Entities;
using Constack.Widemapp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Constack.Widemapp.Application.Services.Account.Commands.Login
{
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, LoginAccountModel>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IAutomationDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginAccountCommandHandler(SignInManager<User> signInManager, IAutomationDbContext context, IConfiguration configuration)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<LoginAccountModel> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await GetUser(request);

            var credentials = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!credentials.Succeeded) throw new BadRequestException(ValidatorMessages.IncorrectPassword);

            var (token, expire) = await GetAccessToken(user);

            return new LoginAccountModel
            {
                Token = token,
                ExpireDate = expire
            };
        }

        private async Task<(string token, DateTime expire)> GetAccessToken(User user)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"])), SecurityAlgorithms.HmacSha512);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(await GetIdentityClaims(user)),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = signingCredentials
            };

            return (tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)), tokenDescriptor.Expires.Value);
        }

        private static Task<List<Claim>> GetIdentityClaims(User user)
        {
            List<Claim> claims = new();

            if (user.UserRoles.Select(x => x.Role).FirstOrDefault() is Role role)
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(UserClaimEnums.Role, role.Name ?? string.Empty),
                    new Claim(UserClaimEnums.RoleId, user.Id.ToString() ?? string.Empty)
                });
            }

            claims.AddRange(new List<Claim>
            {
                new Claim(UserClaimEnums.Id, user.Id.ToString() ?? string.Empty),
                new Claim(UserClaimEnums.UserName, user.UserName ?? string.Empty),
                new Claim(UserClaimEnums.Email, user.Email ?? string.Empty),
                new Claim(UserClaimEnums.FirstName, user.FirstName ?? string.Empty),
                new Claim(UserClaimEnums.LastName, user.LastName ?? string.Empty)
            });

            return Task.FromResult(claims);
        }

        private async Task<User> GetUser(LoginAccountCommand request)
        {
            User user;

            if (new Regex(ValidatorRegex.Email).IsMatch(request.EmailOrUsername))
                user = await _context.Users.AsNoTracking().Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email.ToLower() == request.EmailOrUsername.ToLower());
            else
                user = await _context.Users.AsNoTracking().Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.UserName.ToLower() == request.EmailOrUsername.ToLower());

            return user ?? throw new BadRequestException(ValidatorMessages.NotFound($"Username or Email {request.EmailOrUsername}"));
        }
    }
}

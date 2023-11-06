using Constack.Widemapp.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Constack.Widemapp.Common.Helpers
{
    public static class AuthHelper
    {
        private static IHttpContextAccessor _httpContext;

        public static void Configure(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        /// <summary>
        /// Get Claim by type
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns>Claim Value</returns>
        public static string GetClaim(string claimType) => _httpContext.HttpContext.User.Claims.Where(x => x.Type.ToLower() == claimType.ToLower()).Select(x => x.Value).FirstOrDefault();

        /// <summary>
        /// Returns Authenticated User Id;
        /// </summary>
        public static Guid AuthId => _httpContext.HttpContext.User.Claims.Where(x => x.Type == UserClaimEnums.Id).Select(x => Guid.Parse(x.Value)).FirstOrDefault();

        /// <summary>
        /// Checks if has role by name
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns></returns>
        public static bool HasRole(string roleName) => GetClaim(UserClaimEnums.Role).ToLower() == roleName.ToLower();

        /// <summary>
        ///  Checks if has role by id
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <returns></returns>
        public static bool HasRole(Guid roleId) => Guid.Parse(GetClaim(UserClaimEnums.RoleId)) == roleId;
    }
}

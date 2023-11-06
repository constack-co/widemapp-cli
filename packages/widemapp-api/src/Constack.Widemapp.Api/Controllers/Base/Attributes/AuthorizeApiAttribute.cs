using Constack.Widemapp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection;

namespace Constack.Widemapp.Api.Controllers.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeApiAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeApiAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (HasAnnonymouseAttribute(context)) return;

            var roleClaim = context.HttpContext.User.Claims.Where(x => x.Type == UserClaimEnums.Role).Select(x => x.Value).FirstOrDefault();

            if (roleClaim == null)
            {
                SetUnauthorized(context);

                return;
            }

            if (_roles.Any()) CheckWithRole(roleClaim, context);
        }

        private static bool HasAnnonymouseAttribute(AuthorizationFilterContext context)
        {
            MethodInfo method = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;

            if (method.GetCustomAttribute(typeof(AllowAnonymousAttribute), false) != null) return true;

            else return false;
        }

        private void CheckWithRole(string roleName, AuthorizationFilterContext context)
        {
            if (!_roles.Contains(roleName)) SetForbidden(context);
        }

        private static void SetUnauthorized(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new { message = "Unauthorized" });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        private static void SetForbidden(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new { message = "You don't have permission for this" });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        }
    }
}

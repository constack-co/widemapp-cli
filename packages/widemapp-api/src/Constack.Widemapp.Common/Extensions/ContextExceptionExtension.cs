using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Constack.Widemapp.Common.Extensions
{
    public static class ContextExceptionExtension
    {
        public static void ExceptionFilerResponse(this ExceptionContext context, HttpStatusCode statusCode)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new { context.Exception.Message });
            context.HttpContext.Response.StatusCode = (int)statusCode;
        }
    }
}

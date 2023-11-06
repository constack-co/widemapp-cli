using Constack.Widemapp.Common.Exceptions;
using Constack.Widemapp.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Constack.Widemapp.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadRequestException) context.ExceptionFilerResponse(HttpStatusCode.BadRequest);

            else if (context.Exception is ForbiddenException) context.ExceptionFilerResponse(HttpStatusCode.Forbidden);

            else context.ExceptionFilerResponse(HttpStatusCode.InternalServerError);
        }
    }
}

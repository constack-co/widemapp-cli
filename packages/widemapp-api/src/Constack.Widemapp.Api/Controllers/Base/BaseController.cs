using Constack.Widemapp.Api.Controllers.Base.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers.Base
{
    [ApiController]
    [AuthorizeApi]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}

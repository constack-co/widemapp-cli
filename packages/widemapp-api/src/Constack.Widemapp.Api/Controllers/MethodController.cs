using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Methods.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/methods")]
    public class MethodController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetMethods([FromQuery] GetAllMethodsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}

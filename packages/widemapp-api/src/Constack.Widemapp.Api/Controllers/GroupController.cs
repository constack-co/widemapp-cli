using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Groups.Queries.GetByName;
using Constack.Widemapp.Application.Services.Groups.Queries.GetByTemplateId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/groups")]
    public class GroupController : BaseController
    {
        [HttpGet("template")]
        public async Task<IActionResult> GetGroupsByTemplateId([FromQuery] GetGroupsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("by-name")]
        public async Task<IActionResult> GetGroupsByName([FromQuery] GetByNameCommand query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}

using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Plans.Commands.Finish;
using Constack.Widemapp.Application.Services.Plans.Queries.Get;
using Constack.Widemapp.Application.Services.Plans.Queries.GetById;
using Constack.Widemapp.Application.Services.Plans.Queries.GetGenerationTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/plans")]
    public class PlanController : BaseController
    {
        [HttpPost("finish")]
        public async Task<IActionResult> FinishPlan([FromBody] FinishPlanCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlans([FromQuery] GetPlansQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> GetPlanById([FromQuery] GetPlanByIdQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("generation-types")]
        public async Task<IActionResult> GetGenerationTypes([FromQuery] GetGenerationTypesQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}

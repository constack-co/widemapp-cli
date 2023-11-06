using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Entities.Commands.Add;
using Constack.Widemapp.Application.Services.Entities.Commands.AddProperty;
using Constack.Widemapp.Application.Services.Entities.Commands.AddRelation;
using Constack.Widemapp.Application.Services.Entities.Commands.RemoveRelation;
using Constack.Widemapp.Application.Services.Entities.Commands.UpdateProperty;
using Constack.Widemapp.Application.Services.Entities.Queries.Get;
using Constack.Widemapp.Application.Services.Entities.Queries.GetRelations;
using Constack.Widemapp.Application.Services.PropertyTypes.Queries.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/entities")]
    public class EntityController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetEntitiesApi([FromQuery] GetEntitiesQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("properties")]
        public async Task<IActionResult> AddEntityPropertyApi([FromQuery] GetPropertyTypesQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("property")]
        public async Task<IActionResult> AddEntityPropertyApi([FromBody] AddEntityPropertyCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPut("property")]
        public async Task<IActionResult> UpdateEntityPropertyApi([FromBody] UpdatePropertyCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEntityApi([FromBody] AddEntityCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpGet("relations")]
        public async Task<IActionResult> GetEntityRelationsApi([FromQuery] GetRelationsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpPost("relation")]
        public async Task<IActionResult> AddEntityRealtionApi([FromBody] AddEntityRelationCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpDelete("relation")]
        public async Task<IActionResult> DeleteEntityRealtionApi([FromQuery] DeleteEntityRelationCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }
}

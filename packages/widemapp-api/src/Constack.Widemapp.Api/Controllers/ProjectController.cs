using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Methods.Queries.GetAll;
using Constack.Widemapp.Application.Services.Projects.Commands.Add;
using Constack.Widemapp.Application.Services.Projects.Queries.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/projects")]
    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] GetProjectsQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] AddProjectCommand query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}

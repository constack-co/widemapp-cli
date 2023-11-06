using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Users.Commands.Add;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }
}

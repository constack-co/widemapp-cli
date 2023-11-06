using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Account.Commands.ForgetPassword;
using Constack.Widemapp.Application.Services.Account.Commands.Login;
using Constack.Widemapp.Application.Services.Account.Commands.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAccountCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetAccountPasswordCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetAccountPasswordCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }
}

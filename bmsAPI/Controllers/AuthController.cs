using bms.Application.Features.UserRegistration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bmsAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await mediator.Send(command);
            
            if (result.IsFailure)
            {
                return BadRequest(result.Error?.Message);
            }
            
            return Ok(new { token = result.Value });
        }
    }
}

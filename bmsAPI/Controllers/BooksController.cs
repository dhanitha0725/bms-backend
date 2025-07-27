using bms.Application.Features.AddBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bmsAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    //[Authorize]
    public class BooksController(IMediator mediator) : ControllerBase
    {
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
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
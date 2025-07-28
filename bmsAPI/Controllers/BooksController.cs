using bms.Application.Features.AddBook;
using bms.Application.Features.DeleteBook;
using bms.Application.Features.GetAllBooks;
using bms.Application.Features.GetBookById;
using bms.Application.Features.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            return Ok(new { bookId = result.Value });
        }

        [HttpGet("get-all-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var query = new GetAllBooksQuery();
            var result = await mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error?.Message });
            }

            return Ok(new { books = result.Value});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var query = new GetBookByIdQuery
            {
                BookId = id
            };

            var result = await mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error?.Message });
            }

            return Ok(new { book = result.Value });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand command)
        {
            command.BookId = id;

            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error?.Message });
            }

            return Ok(new {bookId = id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var command = new DeleteBookCommand
            {
                BookId = id
            };

            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { error = result.Error?.Message });
            }

            return Ok(new { message = "Book deleted successfully" });
        }
    }

}
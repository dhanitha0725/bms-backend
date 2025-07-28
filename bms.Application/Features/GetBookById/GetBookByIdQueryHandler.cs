using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bms.Application.Abstractions.Interfaces;
using bms.Application.Features.GetAllBooks;
using bms.Domain.Common;
using bms.Domain.Entities;
using MediatR;
using Serilog;

namespace bms.Application.Features.GetBookById
{
    public class GetBookByIdQueryHandler(
        IGenericRepository<Book, Guid> bookRepository,
        ILogger logger)
        : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
    {
        public async Task<Result<BookDto>> Handle(
            GetBookByIdQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get book by ID from repository
                var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);

                if (book == null)
                {
                    return Result<BookDto>.Failure(new Error("Book not found"));
                }

                // Map to DTO
                var bookDto = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedYear = book.PublishedYear,
                    Genre = book.Genre,
                };

                return Result<BookDto>.Success(bookDto);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while retrieving book {BookId}", request.BookId);
                return Result<BookDto>.Failure(new Error("Failed to retrieve book"));
            }
        }
    }
}

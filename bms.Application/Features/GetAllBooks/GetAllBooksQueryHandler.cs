using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bms.Application.Abstractions.Interfaces;
using bms.Domain.Common;
using bms.Domain.Entities;
using MediatR;
using Serilog;

namespace bms.Application.Features.GetAllBooks
{
    public class GetAllBooksQueryHandler(
        IGenericRepository<Book, Guid> bookRepository,
        ILogger logger) 
        : IRequestHandler<GetAllBooksQuery, Result<List<BookDto>>>
    {
        public async Task<Result<List<BookDto>>> Handle(
            GetAllBooksQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                // Get all books from repository
                var books = await bookRepository.GetAllAsync(cancellationToken);

                // Map to DTOs
                var bookDtos = books.Select(book => new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedYear = book.PublishedYear,
                    Genre = book.Genre,
                    UserId = book.UserId
                });

                return Result<List<BookDto>>.Success(bookDtos.ToList());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while retrieving books");
                return Result<List<BookDto>>.Failure(new Error("Failed to retrieve books"));
            }
        }
    }
}

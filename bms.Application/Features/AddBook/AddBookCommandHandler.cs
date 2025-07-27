using bms.Application.Abstractions.Interfaces;
using bms.Domain.Common;
using bms.Domain.Entities;
using MediatR;
using Serilog;

namespace bms.Application.Features.AddBook
{
    public class AddBookCommandHandler(
        IGenericRepository<Book, Guid> bookRepository,
        IGenericRepository<User, Guid> userRepository,
        ILogger logger) 
        : IRequestHandler<AddBookCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(
            AddBookCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                // Create the book entity
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Author = request.Author,
                    PublishedYear = request.PublishedYear,
                    Genre = request.Genre,

                };

                // Add the book
                await bookRepository.AddAsync(book, cancellationToken);

                return Result<Guid>.Success(book.Id);
            }
            catch (Exception e)
            {
                logger.Error(e, "An error occurred while adding a book: {Title}", request.Title);
                return Result<Guid>.Failure(new Error("An error occurred while adding the book."));
            }
        }
    }
}

using bms.Application.Abstractions.Interfaces;
using bms.Domain.Common;
using bms.Domain.Entities;
using MediatR;
using Serilog;

namespace bms.Application.Features.DeleteBook
{
    public class DeleteBookCommandHandler(
        IGenericRepository<Book, Guid> bookRepository,
        ILogger logger) 
        : IRequestHandler<DeleteBookCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteBookCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                // First, check if the book exists
                var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
                
                if (book == null)
                {
                    logger.Warning("Selected book did not found: {BookId}", request.BookId);
                    return Result.Failure(new Error("Book not found."));
                }

                // Delete the book
                await bookRepository.DeleteAsync(request.BookId, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while deleting book {BookId}", request.BookId);
                return Result.Failure(new Error("Failed to delete book."));
            }
        }
    }
}

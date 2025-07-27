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

namespace bms.Application.Features.UpdateBook
{
    public class UpdateBookCommandHandler(
        IGenericRepository<Book, Guid> bookRepository,
        ILogger logger) 
        : IRequestHandler<UpdateBookCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(
            UpdateBookCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                // First, check if the book exists
                var existingBook = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
                
                if (existingBook == null)
                {
                    logger.Warning("Book did not found: {BookId}", request.BookId);
                    return Result<bool>.Failure(new Error("Book not found."));
                }

                // Update the book properties
                existingBook.Title = request.Title;
                existingBook.Author = request.Author;
                existingBook.PublishedYear = request.PublishedYear;
                existingBook.Genre = request.Genre;

                // Update the book in repository
                await bookRepository.UpdateAsync(existingBook, cancellationToken);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while updating book {BookId}", request.BookId);
                return Result<bool>.Failure(new Error("Failed to update book."));
            }
        }
    }
}

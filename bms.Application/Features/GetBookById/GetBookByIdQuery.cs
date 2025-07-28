using System;
using bms.Application.Features.GetAllBooks;
using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.GetBookById
{
    public class GetBookByIdQuery : IRequest<Result<BookDto>>
    {
        public Guid BookId { get; set; }
    }
}

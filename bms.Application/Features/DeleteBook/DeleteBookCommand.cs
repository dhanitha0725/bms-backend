using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.DeleteBook
{
    public class DeleteBookCommand : IRequest<Result>
    {
        public Guid BookId { get; set; }
    }
}

using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<Result<List<BookDto>>>
    {
    }

    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public string Genre { get; set; }
    }
}

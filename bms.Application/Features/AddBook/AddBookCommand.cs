using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.AddBook
{
    public class AddBookCommand : IRequest<Result<Guid>>
    {
        public string Title { get; set; } 
        public string Author { get; set; } 
        public int PublishedYear { get; set; }
        public string Genre { get; set; } 
    }
}

using System;
using System.Text.Json.Serialization;
using bms.Domain.Common;
using MediatR;

namespace bms.Application.Features.UpdateBook
{
    public class UpdateBookCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public Guid BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public string Genre { get; set; } = string.Empty;
    }
}

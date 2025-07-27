using FluentValidation;

namespace bms.Application.Features.GetAllBooks
{
    public class GetAllBooksQueryValidator : AbstractValidator<GetAllBooksQuery>
    {
        public GetAllBooksQueryValidator()
        {
            // No validation rules needed for this simple query
            // This validator exists for consistency with the pattern
        }
    }
}
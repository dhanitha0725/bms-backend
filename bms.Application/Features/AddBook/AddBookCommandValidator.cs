using FluentValidation;

namespace bms.Application.Features.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .Length(1, 200)
                .WithMessage("Title must be between 1 and 200 characters.");

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("Author is required.")
                .Length(1, 200)
                .WithMessage("Author must be between 1 and 200 characters.");

            RuleFor(x => x.PublishedYear)
                .GreaterThan(0)
                .WithMessage("Published year must be greater than 0.")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Published year cannot be in the future.")
                .GreaterThanOrEqualTo(1000)
                .WithMessage("Published year must be a valid 4-digit year.");

            RuleFor(x => x.Genre)
                .NotEmpty()
                .WithMessage("Genre is required.")
                .Length(1, 50)
                .WithMessage("Genre must be between 1 and 50 characters.");
        }
    }
}

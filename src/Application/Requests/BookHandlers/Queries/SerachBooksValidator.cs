namespace Application.Requests.BookHandlers.Queries;

public class SerachBooksValidator : AbstractValidator<SerachBooksQuery>
{
    public SerachBooksValidator()
    {
        RuleFor(v => v.AuthorName)
            .MaximumLength(250);
        RuleFor(v => v.SearchText)
            .MaximumLength(1000);
        RuleFor(v => v.CombinationCondition).IsInEnum();
    }
}

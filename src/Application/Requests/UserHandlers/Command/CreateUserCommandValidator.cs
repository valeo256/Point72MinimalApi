namespace Application.Requests.UserHandlers.Command;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.FullName)
            .MinimumLength(2)
            .NotEmpty();
    }
}

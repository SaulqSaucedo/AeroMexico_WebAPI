namespace Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.LastName)
             .NotEmpty()
             .MaximumLength(50)
             .WithName("Last Name");

        RuleFor(r => r.Email)
             .NotEmpty()
             .EmailAddress()
             .MaximumLength(255);
    }
}
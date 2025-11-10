namespace Application.Users.Create;

public record CreateUserCommand(
    string Name,
    string LastName,
    string Email) : IRequest<ErrorOr<Guid>>;
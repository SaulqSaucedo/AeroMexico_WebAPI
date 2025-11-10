using Users.Common;

namespace Application.Users.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<ErrorOr<UserResponse>>;
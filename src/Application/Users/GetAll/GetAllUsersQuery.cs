using Users.Common;

namespace Application.Users.GetAll;

public record GetAllUsersQuery() : IRequest<ErrorOr<IReadOnlyList<UserResponse>>>;
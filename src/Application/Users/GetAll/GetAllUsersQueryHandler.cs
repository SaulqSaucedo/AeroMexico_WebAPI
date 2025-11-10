using Users.Common;
using Domain.Users;

namespace Application.Users.GetAll;


internal sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<IReadOnlyList<UserResponse>>>
{
    private readonly IUserRepository _customerRepository;

    public GetAllUsersQueryHandler(IUserRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<UserResponse>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<User> Users = await _customerRepository.GetAll();

        return Users.Select(customer => new UserResponse(
                customer.Id.Value,
                customer.FullName,
                customer.Email,
                customer.Active)).ToList();
    }
}
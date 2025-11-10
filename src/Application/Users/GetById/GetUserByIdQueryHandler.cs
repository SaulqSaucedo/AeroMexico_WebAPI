using Users.Common;
using Domain.Users;

namespace Application.Users.GetById;


internal sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _customerRepository;

    public GetUserByIdQueryHandler(IUserRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _customerRepository.GetByIdAsync(new UserId(query.Id)) is not User customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        return new UserResponse(
            customer.Id.Value, 
            customer.FullName, 
            customer.Email,
            customer.Active);
    }
}
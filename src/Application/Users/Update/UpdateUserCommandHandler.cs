using Domain.Users;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserCommandHandler(IUserRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (!await _customerRepository.ExistsAsync(new UserId(command.Id)))
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            return Error.Validation("Customer.PhoneNumber", "Phone number has not valid format.");
        }

        if (Address.Create(command.Country, command.Line1, command.Line2, command.City,
                    command.State, command.ZipCode) is not Address address)
        {
            return Error.Validation("Customer.Address", "Address is not valid.");
        }

        User customer = User.UpdateCustomer(command.Id, command.Name,
            command.LastName,
            command.Email,
            command.Active);

        _customerRepository.Update(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

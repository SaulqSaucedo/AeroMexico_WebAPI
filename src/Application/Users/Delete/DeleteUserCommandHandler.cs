using Domain.Users;
using Domain.Primitives;

namespace Application.Users.Delete;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserCommandHandler(IUserRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        if (await _customerRepository.GetByIdAsync(new UserId(command.Id)) is not User customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        _customerRepository.Delete(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

using Domain.Users;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;

namespace Application.Users.Create;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserCommandHandler(IUserRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var customer = new User(
            new UserId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            command.Email,
            true
        );

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}

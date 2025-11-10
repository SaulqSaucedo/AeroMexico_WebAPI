using Application.Users.Create;
using Domain.Users;
using Domain.Primitives;
using Domain.DomainErrors;

namespace Application.Users.UnitTests.Create;

public class CreateUserCommandHandlerUnitTests
{
    private readonly Mock<IUserRepository> _mockCustomerRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerUnitTests()
    {
        _mockCustomerRepository = new Mock<IUserRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        
        _handler = new CreateUserCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        //Arrange
        // Se configura los parametros de entrada de nuestra prueba unitaria.
        CreateUserCommand command = new ("Fernando", "Ventura", "fe939@mc.com");
        //Act
        // Se ejecuta el metodo a probar de nuestra prueba unitaria
        var result = await _handler.Handle(command, default);
        //Assert
        // Se verifica los datos de retorno de nuestro metodo probado en la prueba unitaria
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Code);
        result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Description);
    }
}
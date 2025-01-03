using FluentValidation;
using FluentValidation.Results;
using Moq;
using Winecellar.Application.Commands.Identity;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Identity;

namespace Winecellar.Test.Commands.Identity
{
    public class RegisterUserTests
    {
        private readonly Mock<IIdentityRepository> _identityRepositoryMock;
        private readonly Mock<IPasswordHandler> _passwordHandlerMock;
        private readonly Mock<IValidator<RegisterUserRequestDto>> _validatorMock;
        private readonly RegisterUserCommandHandler _handler;
        public RegisterUserTests()
        {
            _identityRepositoryMock = new Mock<IIdentityRepository>();
            _passwordHandlerMock = new Mock<IPasswordHandler>();
            _validatorMock = new Mock<IValidator<RegisterUserRequestDto>>();
            _handler = new RegisterUserCommandHandler(_identityRepositoryMock.Object, _passwordHandlerMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task RegisterUser_WithValidRequest_RegistersUser()
        {
            var user = new RegisterUserRequestDto()
            {
                Username = "test",
                Email = "test@mail.com",
                Password = "12345678"
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _passwordHandlerMock
                .Setup(ph => ph.HashPassword(user.Password))
                .Returns("hashedPassword");

            await _handler.Handle(new RegisterUserCommand(user), CancellationToken.None);

            _validatorMock.Verify(validator => validator.ValidateAsync(user, It.IsAny<CancellationToken>()), Times.Once);
            _passwordHandlerMock.Verify(handler => handler.HashPassword(user.Password), Times.Once);
            _identityRepositoryMock.Verify(repo => repo.RegisterUser(user.Email, user.Username, "hashedPassword"), Times.Once);
        }

        [Fact]
        public async Task RegisterUser_WithInvalidRequest_ThrowsValidationException()
        {
            var user = new RegisterUserRequestDto()
            {
                Username = "test",
                Email = "testmail.com",
                Password = "1234567"
            };

            var validationErrors = new[]
            {
                new ValidationFailure(nameof(RegisterUserRequestDto.Password), "Password must be at least 8 characters long"),
                new ValidationFailure(nameof(RegisterUserRequestDto.Email), "Invalid email format")
            };

            _validatorMock.Setup(v => v.ValidateAsync(user, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult(validationErrors));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new RegisterUserCommand(user), CancellationToken.None));

            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(RegisterUserRequestDto.Email) && e.ErrorMessage == "Invalid email format");
            Assert.Contains(exception.Errors, e => e.PropertyName == nameof(RegisterUserRequestDto.Password) && e.ErrorMessage == "Password must be at least 8 characters long");

            _identityRepositoryMock.Verify(repo => repo.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
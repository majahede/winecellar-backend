
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Winecellar.Api.Controllers;
using Winecellar.Application.Commands.Identity;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Test.Controllers
{
    public class IdentityControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IdentityController _controller;

        public IdentityControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new IdentityController(_mediatorMock.Object);
        }

        [Fact]
        public async Task RegisterAccount_WithValidRequest_ReturnsOk()
        {
            var user = new RegisterUserRequestDto()
            {
                Email = "test@mail.com",
                Username = "test",
                Password = "12345678"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default)).Returns(Task.CompletedTask);

            var result = await _controller.RegisterAccount(user);

            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Successfully registered", okResult.Value);

            _mediatorMock.Verify(m => m.Send(It.Is<RegisterUserCommand>(cmd => cmd.user == user), default), Times.Once);
        }

        [Fact]
        public async Task RegisterAccount_WithInvalidModel_ReturnsBadRequest()
        {
            var invalidRequest = new RegisterUserRequestDto()
            {
                Email = "test",
                Username = "test",
                Password = "12345678"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default))
                 .ThrowsAsync(new ValidationException("Validation error occurred"));

            var result = await Assert.ThrowsAsync<ValidationException>(() => _controller.RegisterAccount(invalidRequest));

            Assert.NotNull(result);
            Assert.Equal("Validation error occurred", result.Message);

        }

        [Fact]
        public async Task Login_WithValidRequest_ReturnsTokens()
        {
            var user = new LoginUserRequestDto()
            {
                LoginInput = "test@mail.com",
                Password = "12345678"
            };

            var tokenDto = new TokenDto()
            {
                AccessToken = "token",
                RefreshToken = "token"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), default)).ReturnsAsync(tokenDto);

            var result = await _controller.Login(user);

            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(tokenDto, okResult.Value);

            _mediatorMock.Verify(m => m.Send(It.Is<LoginUserCommand>(cmd => cmd.user == user), default), Times.Once);
        }

        [Fact]
        public async Task Login_WithInvalidModel_ReturnsUnauthorizedException()
        {
            var user = new LoginUserRequestDto()
            {
                LoginInput = "test@mail.com",
                Password = "12345678"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), default))
                 .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

            var result = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _controller.Login(user));

            Assert.NotNull(result);
            Assert.Equal("Invalid credentials", result.Message);

        }
    }
}
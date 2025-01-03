using Moq;
using System.Security.Claims;
using Winecellar.Application.Commands.Identity;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Token;
using Winecellar.Domain.Models;
using Winecellar.Infrastructure.Security.Interfaces;

namespace Winecellar.Test.Commands.Identity
{
    public class LoginUserTests
    {
        private readonly Mock<IIdentityRepository> _identityRepositoryMock;
        private readonly Mock<ITokenHandler> _tokenHandlerMock;
        private readonly Mock<IPasswordHandler> _passwordHandlerMock;
        private readonly LoginUserCommandHandler _handler;
        public LoginUserTests()
        {
            _identityRepositoryMock = new Mock<IIdentityRepository>();
            _tokenHandlerMock = new Mock<ITokenHandler>();
            _passwordHandlerMock = new Mock<IPasswordHandler>();
            _handler = new LoginUserCommandHandler(_identityRepositoryMock.Object, _passwordHandlerMock.Object, _tokenHandlerMock.Object);
        }

        [Fact]
        public async Task LoginUser_WithValidRequest_ReturnTokens()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "test",
                Email = "test@mail.com",
                Password = "password"
            };

            var loginInfo = new LoginUserRequestDto()
            {
                LoginInput = "test",
                Password = "password"
            };

            var tokenDto = new TokenDto()
            {
                AccessToken = "accesstoken",
                RefreshToken = "refreshtoken"
            };


            _identityRepositoryMock
            .Setup(repo => repo.GetByUsernameOrEmail(loginInfo.LoginInput))
            .ReturnsAsync(user);

            _passwordHandlerMock
            .Setup(handler => handler.VerifyPassword(user.Password, "password"))
            .Returns(true);

            _tokenHandlerMock
            .Setup(handler => handler.GenerateAccessToken(It.IsAny<IEnumerable<Claim>>()))
            .Returns(tokenDto.AccessToken);

            _tokenHandlerMock
                .Setup(handler => handler.GenerateRefreshToken(user.Email))
                .Returns(tokenDto.RefreshToken);

            var result = await _handler.Handle(new LoginUserCommand(loginInfo), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(tokenDto.AccessToken, result.AccessToken);
            Assert.Equal(tokenDto.RefreshToken, result.RefreshToken);
            _identityRepositoryMock.Verify(repo => repo.StoreRefreshToken(tokenDto.RefreshToken, user.Id), Times.Once);
        }

        [Fact]
        public async Task LoginUser_UserDoesNotExist_ThrowsUnauthorizedAccessException()
        {

            var loginInfo = new LoginUserRequestDto()
            {
                LoginInput = "test",
                Password = "password"
            };

            _identityRepositoryMock
            .Setup(repo => repo.GetByUsernameOrEmail(loginInfo.LoginInput))
            .ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _handler.Handle(new LoginUserCommand(loginInfo), CancellationToken.None));

            Assert.Contains("Invalid credentials", exception.Message);
            _identityRepositoryMock.Verify(r => r.StoreRefreshToken(It.IsAny<string>(), It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task LoginUser_InvalidPassword_ThrowsUnauthorizedAccessException()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "test",
                Email = "test@mail.com",
                Password = "password"
            };

            var loginInfo = new LoginUserRequestDto()
            {
                LoginInput = "test",
                Password = "password"
            };

            _identityRepositoryMock
           .Setup(repo => repo.GetByUsernameOrEmail(loginInfo.LoginInput))
           .ReturnsAsync(user);

            _passwordHandlerMock
            .Setup(handler => handler.VerifyPassword(user.Password, "hashedpassword"))
            .Returns(false);

            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _handler.Handle(new LoginUserCommand(loginInfo), CancellationToken.None));

            Assert.Contains("Invalid credentials", exception.Message);
            _identityRepositoryMock.Verify(r => r.StoreRefreshToken(It.IsAny<string>(), user.Id), Times.Never);
        }
    }
}
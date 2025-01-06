using Winecellar.Infrastructure.Repositories;

namespace Winecellar.Test.Repositories
{
    public class IdentityRepositoryTests : RepositoryTestBase
    {
        private readonly IdentityRepository _repository;
        public IdentityRepositoryTests()
        {
            _repository = new IdentityRepository(DbConnectionFactory);
        }

        [Fact]
        public async Task RegisterUser_ValidInput_ShouldInsertUserAndReturnId()
        {
            var email = "test@test.com";
            var username = "test";
            var password = "password";

            var userId = await _repository.RegisterUser(email, username, password);

            var user = await _repository.GetByUsernameOrEmail(username);

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal(username, user.Username);
        }
    }
}

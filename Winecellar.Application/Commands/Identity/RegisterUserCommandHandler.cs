using MediatR;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Application.Commands.Identity
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHandler _passwordHandler;

        public RegisterUserCommandHandler(IIdentityRepository identityRepository, IPasswordHandler passwordHandler)
        {
            _identityRepository = identityRepository;
            _passwordHandler = passwordHandler;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var hashedPassword = _passwordHandler.HashPassword(request.user.Password);

            await _identityRepository.RegisterUser(request.user.Email, request.user.Username, hashedPassword);

        }
    }
}

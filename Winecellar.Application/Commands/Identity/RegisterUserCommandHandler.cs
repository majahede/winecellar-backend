using MediatR;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Application.Commands.Identity
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IIdentityRepository _identityRepository;

        public RegisterUserCommandHandler(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}

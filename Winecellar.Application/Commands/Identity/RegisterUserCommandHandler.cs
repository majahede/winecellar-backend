using FluentValidation;
using MediatR;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Token;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Commands.Identity
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHandler _passwordHandler;
        private readonly IValidator<RegisterUserRequestDto> _validator;

        public RegisterUserCommandHandler(IIdentityRepository identityRepository, IPasswordHandler passwordHandler, IValidator<RegisterUserRequestDto> validator)
        {
            _identityRepository = identityRepository;
            _passwordHandler = passwordHandler;
            _validator = validator;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.user, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var hashedPassword = _passwordHandler.HashPassword(request.user.Password);

            await _identityRepository.RegisterUser(request.user.Email, request.user.Username, hashedPassword);

        }
    }
}

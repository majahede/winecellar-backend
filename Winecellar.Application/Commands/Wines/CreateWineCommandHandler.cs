using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Winecellar.Application.Commands.Wines;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Wines;

namespace Winecellar.Application.Commands.Wines
{
    public class CreateWineCommandHandler : IRequestHandler<CreateWineCommand, Guid>
    {
        private readonly IWineRepository _wineRepository;
        private readonly IValidator<CreateWineRequestDto> _validator;

        public CreateWineCommandHandler(IWineRepository wineRepository, IValidator<CreateWineRequestDto> validator)
        {
            _wineRepository = wineRepository;
            _validator = validator;
        }

        public async Task<Guid> Handle(CreateWineCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.wine, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _wineRepository.CreateWine(request.wine);
        }
    }
}
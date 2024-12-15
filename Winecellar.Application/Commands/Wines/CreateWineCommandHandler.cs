using MediatR;
using Microsoft.Extensions.Logging;
using Winecellar.Application.Commands.Wines;
using Winecellar.Application.Common.Interfaces;

namespace Winecellar.Application.Commands.Wines
{
    public class CreateWineCommandHandler : IRequestHandler<CreateWineCommand, Guid>
    {
        private readonly IWineRepository _wineRepository;

        public CreateWineCommandHandler(IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }

        public async Task<Guid> Handle(CreateWineCommand request, CancellationToken cancellationToken)
        {
                return await _wineRepository.CreateWine(request.wine);
        }
    }
}
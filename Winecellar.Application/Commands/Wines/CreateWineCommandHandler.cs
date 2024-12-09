using MediatR;
using Winecellar.Application.Common.Interfaces;

namespace Winecellar.Application.Commands.Wines
{
    public class CreateWineCommandHandler : IRequestHandler<CreateWineCommand>
    {
        private readonly IWineRepository _wineRepository;

        public CreateWineCommandHandler(IWineRepository wineRepository) => _wineRepository = wineRepository;

        public async Task Handle(CreateWineCommand request, CancellationToken cancellationToken)
        {
            await _wineRepository.CreateWine(request.wine);

            return;
        }
    }
}

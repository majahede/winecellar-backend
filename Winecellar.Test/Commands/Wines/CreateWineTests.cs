using FluentValidation;
using FluentValidation.Results;
using Moq;
using Winecellar.Application.Commands.Wines;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Wines;

namespace Winecellar.Test.Commands.Wines
{
    public class CreateWineTests
    {
        private readonly Mock<IWineRepository> _wineRepositoryMock;
        private readonly Mock<IValidator<CreateWineRequestDto>> _validatorMock;
        private readonly CreateWineCommandHandler _handler;
        public CreateWineTests()
        {
            _wineRepositoryMock = new Mock<IWineRepository>();
            _validatorMock = new Mock<IValidator<CreateWineRequestDto>>();
            _handler = new CreateWineCommandHandler(_wineRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task CreateWine_WithValidRequest_ReturnGuid()
        {
            var wineId = Guid.NewGuid();

            var wine = new CreateWineRequestDto()
            {
                Name = "Wine A"
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(wine, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _wineRepositoryMock
            .Setup(repo => repo.CreateWine(wine))
            .ReturnsAsync(wineId);

            var result = await _handler.Handle(new CreateWineCommand(wine), CancellationToken.None);

            Assert.Equal(wineId, result);
            _wineRepositoryMock.Verify(r => r.CreateWine(wine), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenValidatorFails()
        {

            var wine = new CreateWineRequestDto()
            {
                Name = ""
            };

            var validationErrors = new[]
            {
                new ValidationFailure(nameof(CreateWineRequestDto.Name), "Name is required")
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(wine, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationErrors));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(new CreateWineCommand(wine), CancellationToken.None));

            Assert.Contains("Name is required", exception.Message);
            _wineRepositoryMock.Verify(r => r.CreateWine(It.IsAny<CreateWineRequestDto>()), Times.Never);
        }
    }
}

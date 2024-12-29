using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Winecellar.Application.Commands.Wines;
using Winecellar.Application.Dtos.Wines;
using Winecellar.Application.Queries.Wines;
using Winecellar.Controllers;
using Winecellar.Domain.Models;

namespace Winecellar.Test.Controllers
{
    public class WineControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WineController _controller;

        public WineControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new WineController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllWines_WithWineList_ReturnsOk()
        {
            var wineList = new List<Wine>
            {
                new Wine { Id = Guid.NewGuid(), Name = "Wine A" },
                new Wine { Id = Guid.NewGuid(), Name = "Wine B" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllWinesQuery>(), default))
                .ReturnsAsync(wineList);

            var result = await _controller.GetWines();
            
            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(wineList, okResult.Value);
        }

        [Fact]
        public async Task GetWines_WithEmptyList_ReturnsOk()
        {
            var emptyWineList = new List<Wine>();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWinesQuery>(), default))
                         .ReturnsAsync(emptyWineList);

            var result = await _controller.GetWines();

            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(emptyWineList, okResult.Value);
        }

        [Fact]
        public async Task GetWines_ThrowsException_Returns500()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWinesQuery>(), default))
                         .ThrowsAsync(new Exception("Something went wrong"));

            await Assert.ThrowsAsync<Exception>(() => _controller.GetWines());
        }

        [Fact]
        public async Task CreateWine_WithValidRequest_ReturnsGuid()
        {
            var wineRequest = new CreateWineRequestDto { Name = "Wine A" };
            var newWineId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateWineCommand>(), default))
                .ReturnsAsync(newWineId);

            var result = await _controller.CreateWine(wineRequest);

            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(newWineId, okResult.Value);

            _mediatorMock.Verify(m => m.Send(It.Is<CreateWineCommand>(cmd => cmd.wine == wineRequest), default), Times.Once);
        }

        [Fact]
        public async Task CreateWine_WithInvalidModel_ReturnsBadRequest()
        {
            var invalidWine = new CreateWineRequestDto
            {
                Name = ""
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateWineCommand>(), default))
                 .ThrowsAsync(new ValidationException("Validation error occurred"));

            var result = await Assert.ThrowsAsync<ValidationException>(() => _controller.CreateWine(invalidWine));

            Assert.NotNull(result);
            Assert.Equal("Validation error occurred", result.Message);

        }
    }
}
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
        public async Task GetAllWines_WithValidRequest_ReturnOkWithWineList()
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

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(wineList, okResult.Value);
        }

        [Fact]
        public async Task CreateWine_WithValidRequest_ShouldReturnGuid()
        {
            var wineRequest = new CreateWineRequestDto { Name = "Wine A" };
            var newWineId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateWineCommand>(), default))
                .ReturnsAsync(newWineId);

            var result = await _controller.CreateWine(wineRequest);

            var actionResult = Assert.IsType<ActionResult<Guid>>(result);
            var guidResult = Assert.IsType<Guid>(actionResult.Value);
            Assert.Equal(newWineId, guidResult);
        }
    }
}
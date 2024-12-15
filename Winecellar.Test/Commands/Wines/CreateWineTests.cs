using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Winecellar.Application.Commands.Wines;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Wines;
using Winecellar.Controllers;
using Winecellar.Domain.Models;

namespace Winecellar.Test.Commands.Wines
{
    public class CreateWineTests
    {
        private readonly Mock<IWineRepository> _wineRepositoryMock;
        private readonly CreateWineCommandHandler _handler;
        public CreateWineTests()
        {
            _wineRepositoryMock = new Mock<IWineRepository>();
            _handler = new CreateWineCommandHandler(_wineRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateWine_WithValidRequest_ReturnGuid()
        {
            var wineId = new Guid();

            var wine = new CreateWineRequestDto()
            {
                Name = "Wine A"
            };

            _wineRepositoryMock
            .Setup(repo => repo.CreateWine(wine))
            .ReturnsAsync(wineId);

            var result = await _handler.Handle(new CreateWineCommand(wine), CancellationToken.None);

            Assert.Equal(wineId, result);
        }
    }
}

using Moq;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using StarWars.Core.Services;
using Xunit;

namespace StarWars.Tests.StarWars.Core.Tests
{
    public class PlanetServiceTests
    {
        [Fact]
        public void AddAsync_Should_Add_Planet_To_Repository()
        {
            _asyncRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Planet>())).ReturnsAsync(() => new Planet { });

            var planet = new Planet
            {
                Id = 1,
                Name = "Planet Test",
                Description = "This is a description of planet test"
            };

            var result = _planetService.AddAsync(planet).Result;

            Assert.NotNull(result);
        }

        public PlanetServiceTests()
        {
            _asyncRepositoryMock = new Mock<IAsyncRepository<Planet>>();
            _planetService = new PlanetService(_asyncRepositoryMock.Object);
        }

        private readonly Mock<IAsyncRepository<Planet>> _asyncRepositoryMock;
        private readonly PlanetService _planetService;
    }
}

using Moq;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using StarWars.Core.Services;
using Xunit;

namespace StarWars.Tests.StarWars.Core.Tests
{
    public class CharacterServiceTests
    {
        [Fact]
        public void AddPlanetToCharacter_Should_Add_Planet_To_Character()
        {
            _asyncRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Character>())).ReturnsAsync(() => null);

            var character = new Character
            {
                Name = "Luke"
            };

            var planet = new Planet
            {
                Name = "Planet X"
            };

            _characterService.AddPlanetToCharacter(character, planet).Wait();

            Assert.NotNull(character.Planet);
        }

        [Fact]
        public void RemovePlanetFromCharacter_Should_Remove_Planet_From_Character()
        {
            _asyncRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Character>())).ReturnsAsync(() => null);

            var character = new Character
            {
                Name = "Luke",
                Planet = new Planet
                {
                    Name = "Planet X"
                }
            };

            _characterService.RemovePlanetFromCharacter(character).Wait();

            Assert.Null(character.Planet);
        }

        public CharacterServiceTests()
        {
            _asyncRepositoryMock = new Mock<IAsyncRepository<Character>>();
            _characterService = new CharacterService(_asyncRepositoryMock.Object);
        }

        private readonly Mock<IAsyncRepository<Character>> _asyncRepositoryMock;
        private readonly CharacterService _characterService;
    }
}

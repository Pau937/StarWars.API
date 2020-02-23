using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarWars.API.Controllers;
using StarWars.API.Mapper;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using Xunit;

namespace StarWars.Tests.StarWars.API.Tests
{
	public class PlanetControllerTests
	{
		[Fact]
		public void AssignPlanetToCharacter_Should_Return_OkResultObject_When_Character_And_Planet_Exists_In_Database()
		{
			characterServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Character { });
			planetServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Planet { });

			var controller = new PlanetController(_mapper, planetServiceMock.Object, characterServiceMock.Object);

			var result = controller.AssignPlanetToCharacter(It.IsAny<int>(), It.IsAny<int>()).Result;

			Assert.IsType<OkObjectResult>(result);
		}

		[Theory]
		[InlineData(false, false)]
		[InlineData(false, true)]
		[InlineData(true, false)]
		public void AssignPlanetToCharacter_Should_Return_NotFoundObjectResult_When_Character_Or_Planet_Not_Exists_In_Database(bool characterExists, bool planetExists)
		{
			characterServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => characterExists ? new Character { } : null);
			planetServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => planetExists ? new Planet { } : null);

			var controller = new PlanetController(_mapper, planetServiceMock.Object, characterServiceMock.Object);

			var result = controller.AssignPlanetToCharacter(It.IsAny<int>(), It.IsAny<int>()).Result;

			Assert.IsType<NotFoundObjectResult>(result);
		}

		public PlanetControllerTests()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfile());
			});

			_mapper = mockMapper.CreateMapper();

			characterServiceMock = new Mock<ICharacterService>();
			planetServiceMock = new Mock<IPlanetService>();
		}

		private readonly IMapper _mapper;

		private Mock<ICharacterService> characterServiceMock;
		private Mock<IPlanetService> planetServiceMock;
	}
}

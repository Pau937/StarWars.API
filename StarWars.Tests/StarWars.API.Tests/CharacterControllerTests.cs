using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarWars.API.Controllers;
using StarWars.API.Dtos;
using StarWars.API.Mapper;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using Xunit;

namespace StarWars.Tests.StarWars.API.Tests
{
	public class CharacterControllerTests
	{
		[Fact]
		public void GetCharacter_Should_Return_OkResultObject_And_CharacterViewDto_Value_If_Character_Exists_In_Database()
		{
			var characterService = new Mock<ICharacterService>();

			characterService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => new Character { });

			var controller = new CharacterController(_mapper, characterService.Object);

			var result = controller.GetCharacter(It.IsAny<int>()).Result;

			Assert.NotNull(result);
			Assert.IsType<OkObjectResult>(result);

			var okObject = result as OkObjectResult;

			Assert.IsType<CharacterViewDto>(okObject.Value);
		}

		public CharacterControllerTests()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfile());
			});

			_mapper = mockMapper.CreateMapper();
		}

		private readonly IMapper _mapper;
	}
}

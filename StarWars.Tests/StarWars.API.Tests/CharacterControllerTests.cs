using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Controllers;
using StarWars.API.Dtos;
using StarWars.API.Mapper;
using Xunit;

namespace StarWars.Tests.StarWars.API.Tests
{
	public class CharacterControllerTests
	{
		[Fact]
		public void GetCharacter_Should_Return_CharacterViewDto()
		{
			var controller = new CharacterController(_mapper);

			var result = controller.GetCharacter(5).Result;			

			Assert.IsType<OkObjectResult>(result);

			var character = result as OkObjectResult;

			Assert.IsType<CharacterViewDto>(character.Value);
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

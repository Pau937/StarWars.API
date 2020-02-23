using Microsoft.AspNetCore.Mvc;
using StarWars.API.Controllers;
using StarWars.Core.Models;
using Xunit;

namespace StarWars.Tests.StarWars.API.Tests
{
	public class CharacterControllerTests
	{
		[Fact]
		public void GetCharacter_Should_Return_Character_By_Given_Id()
		{
			var controller = new CharacterController();

			var result = controller.GetCharacter(5).Result;			

			Assert.IsType<OkObjectResult>(result);

			var character = result as OkObjectResult;

			Assert.NotNull(character.Value);
		}
	}
}

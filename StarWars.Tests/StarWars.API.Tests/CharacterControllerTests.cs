using StarWars.API.Controllers;
using Xunit;

namespace StarWars.Tests.StarWars.API.Tests
{
	public class CharacterControllerTests
	{
		[Fact]
		public void CharacterController_Should_Exists()
		{
			var controller = new CharacterController();

			Assert.NotNull(controller);
		}
	}
}

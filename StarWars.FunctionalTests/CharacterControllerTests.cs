using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using StarWars.API.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace StarWars.FunctionalTests
{
	public class CharacterControllerTests : IClassFixture<WebTest>
	{
		[Theory]
		[InlineData(1, 2, 2)]
		[InlineData(2, 1, 1)]
		[InlineData(2, 3, 3)]
		public void GetCharacters_Should_Return_Paginated_Characters(int pageNumber, int pageSize, int expectedElements)
		{
			var response = Client.GetAsync($"/api/character?pageNumber={pageNumber}&pageSize={pageSize}").Result;

			var stringResponse = response.Content.ReadAsStringAsync().Result;

			var model = JsonConvert.DeserializeObject<IList<CharacterViewDto>>(stringResponse);

			Assert.Equal(expectedElements, model.Count);
		}

		[Fact]
		public void GetCharacters_Should_Return_Max_100_Characters_When_PageNumber_And_PageSize_Is_Not_Given()
		{
			var response = Client.GetAsync("/api/character").Result;

			var stringResponse = response.Content.ReadAsStringAsync().Result;

			var model = JsonConvert.DeserializeObject<IList<CharacterViewDto>>(stringResponse);

			Assert.True(model.Count <= 100);
		}

		[Fact]
		public void AddCharacter_Should_Return_CharacterViewDto_Model_With_Id_Not_Equal_Zero()
		{
			string serailizeddto = JsonConvert.SerializeObject(new CharacterDto
			{
				Age = 55,
				Name = "Obi-Wan"
			});

			var inputMessage = new HttpRequestMessage
			{
				Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
			};

			var response = Client.PostAsync("/api/character", inputMessage.Content).Result;

			var stringResponse = response.Content.ReadAsStringAsync().Result;

			var model = JsonConvert.DeserializeObject<CharacterInfoDto>(stringResponse);

			Assert.NotNull(model);
			Assert.True(model.Id != 0);
		}

		public CharacterControllerTests(WebTest factory)
		{
			Client = factory.CreateClient(new WebApplicationFactoryClientOptions());
		}

		public HttpClient Client { get; }
	}
}

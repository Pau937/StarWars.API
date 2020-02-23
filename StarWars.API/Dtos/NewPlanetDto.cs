using System.ComponentModel.DataAnnotations;

namespace StarWars.API.Dtos
{
	public class NewPlanetDto
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;

namespace StarWars.API.Dtos
{
	public class CharacterDto
	{
		[Required]
		[StringLength(16, MinimumLength = 2, ErrorMessage = "Name should have between 2 and 16 characters")]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required]
		public int Age { get; set; }
	}
}

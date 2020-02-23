using System.ComponentModel.DataAnnotations;

namespace StarWars.API.Dtos
{
	public class NewEpisodeDto
	{
		[Required]
		[StringLength(16, MinimumLength = 4, ErrorMessage = "Episode's name should be between 4 and 16 characters")]
		public string Name { get; set; }
		public string Date { get; set; }
	}
}

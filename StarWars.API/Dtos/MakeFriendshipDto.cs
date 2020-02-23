using System.ComponentModel.DataAnnotations;

namespace StarWars.API.Dtos
{
	public class MakeFriendshipDto
	{
		[Required]
		public int FirstCharacterId { get; set; }
		[Required]
		public int SecondCharacterId { get; set; }
	}
}

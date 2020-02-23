using StarWars.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWars.Core.Models
{
	public class Appearance : BaseEntity
	{
		public Character Character { get; set; }
		public Episode Episode { get; set; }
		public int EpisodeId { get; set; }
		public int CharacterId { get; set; }
		[NotMapped]
		public override int Id { get; protected set; }
	}
}

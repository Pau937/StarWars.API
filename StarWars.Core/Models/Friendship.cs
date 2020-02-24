using StarWars.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWars.Core.Models
{
	public class Friendship : BaseEntity
	{
		public Character Character { get; set; }
		public Character Friend { get; set; }
		public int FriendId { get; set; }
		public int CharacterId { get; set; }
		[NotMapped]
		public override int Id { get; set; }
	}
}

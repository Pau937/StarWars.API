using StarWars.Core.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarWars.Core.Models
{
	public class Character : BaseEntity
	{
		public List<Friendship> CharacterFriends { get; set; }
		public List<Friendship> FriendCharacters { get; set; }
		[NotMapped]
		public List<Friendship> Friends { get; set; }
		public List<Appearance> Appearances { get; set; }
		public Planet Planet { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Age { get; set; }
		public string Description { get; set; }

		public Character()
		{
			Friends = new List<Friendship>();
		}
	}
}

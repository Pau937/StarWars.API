﻿using System.Collections.Generic;

namespace StarWars.API.Dtos
{
	public class CharacterViewDto
	{
		public IEnumerable<FriendDto> Friends { get; set; }
		public IEnumerable<AppearanceCharacterDto> Episodes { get; set; }
		public PlanetDto Planet { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int Id { get; set; }
		public string Description { get; set; }
	}
}

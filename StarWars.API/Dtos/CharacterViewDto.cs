namespace StarWars.API.Dtos
{
	public class CharacterViewDto
	{
		public PlanetDto Planet { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public int Id { get; set; }
		public string Description { get; set; }
	}
}

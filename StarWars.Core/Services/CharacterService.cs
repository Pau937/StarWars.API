using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.Services
{
	public class CharacterService : ICharacterService
	{
		public async Task<Character> GetByIdAsync(int id)
		{
			return await _characterRepository.GetByIdAsync(id, new List<string> { "Planet", "Appearances", "Appearances.Episode", "Appearances.Character", "FriendCharacters", "CharacterFriends" });
		}

		public async Task<Character> AddAsync(Character model)
		{
			return await _characterRepository.AddAsync(model);
		}

		public async Task<Character> UpdateAsync(Character model)
		{
			return await _characterRepository.UpdateAsync(model);
		}

		public async Task RemoveAsync(Character model)
		{
			await _characterRepository.RemoveAsync(model);
		}

		public IQueryable<Character> GetAll()
		{
			return _characterRepository.GetAll(new List<string> { "Planet", "Appearances", "Appearances.Episode", "Appearances.Character", "FriendCharacters", "CharacterFriends" });
		}

		public async Task AddPlanetToCharacter(Character character, Planet planet)
		{
			character.Planet = planet;

			await _characterRepository.UpdateAsync(character);
		}

		public async Task RemovePlanetFromCharacter(Character character)
		{
			character.Planet = null;

			await _characterRepository.UpdateAsync(character);
		}

		public CharacterService(IAsyncRepository<Character> characterRepository)
		{
			_characterRepository = characterRepository;
		}

		private readonly IAsyncRepository<Character> _characterRepository;
	}
}

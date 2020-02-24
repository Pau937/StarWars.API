using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Services
{
	public class CharacterService : ICharacterService
	{
		public async Task<Character> GetByIdAsync(int id)
		{
			return await _characterRepository.GetByIdAsync(id);
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

		public async Task<IEnumerable<Character>> GetAll(int skipElements, int takeElements)
		{
			return await _characterRepository.GetAllAsync(skipElements, takeElements);
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

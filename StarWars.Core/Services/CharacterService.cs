using StarWars.Core.Interfaces;
using StarWars.Core.Models;
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

		public CharacterService(IAsyncRepository<Character> characterRepository)
		{
			_characterRepository = characterRepository;
		}

		private readonly IAsyncRepository<Character> _characterRepository;
	}
}

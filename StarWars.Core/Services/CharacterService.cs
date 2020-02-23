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

		public CharacterService(IAsyncRepository<Character> characterRepository)
		{
			_characterRepository = characterRepository;
		}

		private readonly IAsyncRepository<Character> _characterRepository;
	}
}

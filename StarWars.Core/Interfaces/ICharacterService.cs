using StarWars.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface ICharacterService
	{
		Task<Character> GetByIdAsync(int id);
		Task<Character> AddAsync(Character model);
		Task<Character> UpdateAsync(Character model);
		Task RemoveAsync(Character model);
		Task<IEnumerable<Character>> GetAll(int skipElements, int takeElements);
		Task AddPlanetToCharacter(Character character, Planet planet);
		Task RemovePlanetFromCharacter(Character character);
	}
}

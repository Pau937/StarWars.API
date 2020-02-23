using StarWars.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface ICharacterService
	{
		Task<Character> GetByIdAsync(int id);
		Task<Character> AddAsync(Character model);
		Task<Character> UpdateAsync(Character model);
		Task RemoveAsync(Character model);
		IQueryable<Character> GetAll();
		Task AddPlanetToCharacter(Character character, Planet planet);
		Task RemovePlanetFromCharacter(Character character);
	}
}

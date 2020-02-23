using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface ICharacterService
	{
		Task<Character> GetByIdAsync(int id);
	}
}

using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface IPlanetService
	{
		Task<Planet> GetByIdAsync(int id);
		Task RemoveAsync(Planet model);
		Task<Planet> AddAsync(Planet model);
	}
}

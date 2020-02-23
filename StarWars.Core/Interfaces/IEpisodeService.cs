using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.Core.Interfaces
{
	public interface IEpisodeService
	{
		Task<Episode> GetByIdAsync(int id);
		Task RemoveAsync(Episode model);
		Task<Episode> AddAsync(Episode model);
		Task AssignEpisodeToCharacter(Episode episode, Character character);
		Task RemoveEpisodeFromCharacter(int episodeId, int characterId);
	}
}

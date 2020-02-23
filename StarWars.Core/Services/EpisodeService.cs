using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System;
using System.Threading.Tasks;

namespace StarWars.Core.Services
{
	public class EpisodeService : IEpisodeService
	{
		public async Task<Episode> AddAsync(Episode model)
		{
			return await _episodeRepository.AddAsync(model);
		}

		public async Task<Episode> GetByIdAsync(int id)
		{
			return await _episodeRepository.GetByIdAsync(id);
		}

		public async Task RemoveAsync(Episode model)
		{
			await _episodeRepository.RemoveAsync(model);
		}

		public async Task AssignEpisodeToCharacter(Episode episode, Character character)
		{
			var app = new Appearance
			{
				Character = character,
				Episode = episode
			};

			await _appearanceRepository.AddAsync(app);
		}

		public async Task RemoveEpisodeFromCharacter(int episodeId, int characterId)
		{
			var appearance = await _appearanceRepository.GetByCompositeIdAsync(new Tuple<int, int>(episodeId, characterId));

			await _appearanceRepository.RemoveAsync(appearance);
		}

		public EpisodeService(IAsyncRepository<Episode> episodeRepository, IAsyncRepository<Appearance> appearanceRepository)
		{
			_episodeRepository = episodeRepository;
			_appearanceRepository = appearanceRepository;
		}

		private readonly IAsyncRepository<Episode> _episodeRepository;
		private readonly IAsyncRepository<Appearance> _appearanceRepository;
	}
}

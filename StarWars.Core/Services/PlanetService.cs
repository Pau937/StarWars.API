using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.Core.Services
{
	public class PlanetService : IPlanetService
	{
		public async Task<Planet> AddAsync(Planet model)
		{
			return await _planetRepository.AddAsync(model);
		}


		public async Task<Planet> GetByIdAsync(int id)
		{
			return await _planetRepository.GetByIdAsync(id);
		}

		public async Task RemoveAsync(Planet model)
		{
			await _planetRepository.RemoveAsync(model);
		}

		public PlanetService(IAsyncRepository<Planet> planetRepository)
		{
			_planetRepository = planetRepository;
		}

		private readonly IAsyncRepository<Planet> _planetRepository;
	}
}

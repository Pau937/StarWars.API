using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Dtos;
using StarWars.API.Filters;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System.Threading.Tasks;

namespace StarWars.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class EpisodeController : ControllerBase
	{
		[HttpPost]
		[ValidateModelFilter]
		public async Task<IActionResult> AddEpisode(NewEpisodeDto dto)
		{
			Episode model = new Episode();

			_mapper.Map(dto, model);

			var createdEpisode = await _episodeService.AddAsync(model);

			return Created($"localhost:5000/api/episode/{createdEpisode.Id}", _mapper.Map<EpisodeInfoDto>(createdEpisode));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEpisode(int id)
		{
			var episode = await _episodeService.GetByIdAsync(id);

			if (episode == null) return NotFound("Episode with the given id cannot be find in a database.");

			await _episodeService.RemoveAsync(episode);

			return Ok($"Episode with id: {id} has been deleted successfully.");
		}

		[HttpPost("AssignEpisodeToCharacter")]
		public async Task<IActionResult> AssignEpisodeToCharacter(AppearanceDto dto)
		{
			var character = await _characterService.GetByIdAsync(dto.CharacterId);

			if (character == null) return NotFound($"The character with id: {dto.CharacterId} cannot be found in database.");

			var episode = await _episodeService.GetByIdAsync(dto.EpisodeId);

			if (episode == null) return NotFound($"The planet with id: {dto.EpisodeId} cannot be found in database.");

			await _episodeService.AssignEpisodeToCharacter(episode, character);

			return Ok($"Episode with id: {dto.EpisodeId} has been added successfully to character with id: {dto.CharacterId}.");
		}

		[HttpDelete]
		[Route("RemoveEpisodeFromCharacter/{characterId}/{episodeId}")]
		public async Task<IActionResult> RemoveEpisodeFromCharacter(int characterId, int episodeId)
		{
			var character = await _characterService.GetByIdAsync(characterId);

			if (character == null) return NotFound($"The character with id: {characterId} cannot be found in database.");

			var episode = await _episodeService.GetByIdAsync(episodeId);

			if (episode == null) return NotFound($"The episode with id: {episodeId} cannot be found in database.");

			await _episodeService.RemoveEpisodeFromCharacter(episodeId, characterId);

			return Ok($"Episode with id: {episodeId} has been deleted successfully from character with id: {characterId}.");
		}

		public EpisodeController(IEpisodeService episodeService, IMapper mapper, ICharacterService characterService)
		{
			_episodeService = episodeService;
			_mapper = mapper;
			_characterService = characterService;
		}

		private readonly IEpisodeService _episodeService;
		private readonly ICharacterService _characterService;
		private readonly IMapper _mapper;
	}
}

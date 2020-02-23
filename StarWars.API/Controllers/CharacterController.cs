using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Dtos;
using StarWars.API.Filters;
using StarWars.API.Pagination;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CharacterController : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCharacter(int id)
		{
			var character = await _characterService.GetByIdAsync(id);

			if (character == null) return NotFound(CHARACTER_CANNOT_BE_FOUND_MESSAGE);

			var characterDto = _mapper.Map<CharacterViewDto>(character);

			return Ok(characterDto);
		}

		[HttpPost]
		[ValidateModelFilter]
		public async Task<IActionResult> AddCharacter(CharacterDto characterDto)
		{
			Character character = new Character();

			_mapper.Map(characterDto, character);

			var createdCharacter = await _characterService.AddAsync(character);

			return Created($"localhost:5000/api/character/{createdCharacter.Id}", _mapper.Map<CharacterInfoDto>(createdCharacter));
		}

		[HttpPut("{id}")]
		[ValidateModelFilter]
		public async Task<IActionResult> EditCharacter(int id, CharacterDto characterDto)
		{
			var character = await _characterService.GetByIdAsync(id);

			if (character == null) return NotFound(CHARACTER_CANNOT_BE_FOUND_MESSAGE);

			_mapper.Map(characterDto, character);

			var updatedCharacter = await _characterService.UpdateAsync(character);

			return Ok(_mapper.Map<CharacterViewDto>(updatedCharacter));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCharacter(int id)
		{
			var character = await _characterService.GetByIdAsync(id);

			if (character == null) return NotFound(CHARACTER_CANNOT_BE_FOUND_MESSAGE);

			await _characterService.RemoveAsync(character);

			return Ok($"Character with id: {id} has been deleted successfully.");
		}

		[HttpGet]
		public async Task<IActionResult> GetCharacters(PaginationParameters paginationParams)
		{
			var characters = _characterService.GetAll();

			if (!characters.Any()) return NoContent();

			var characterPaginated = await PaginatedList<Character>.CreatePaginatedListAsync(characters, paginationParams.PageNumber, paginationParams.PageSize);

			var characterDtos = characterPaginated.Select(x => _mapper.Map<CharacterViewDto>(x));

			return Ok(characterDtos);
		}

		public CharacterController(IMapper mapper, ICharacterService characterService)
		{
			_mapper = mapper;
			_characterService = characterService;
		}

		private readonly IMapper _mapper;
		private readonly ICharacterService _characterService;
		private const string CHARACTER_CANNOT_BE_FOUND_MESSAGE = "The character with given id cannot be found in database.";
	}
}

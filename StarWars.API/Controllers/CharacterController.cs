using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Dtos;
using StarWars.Core.Interfaces;
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

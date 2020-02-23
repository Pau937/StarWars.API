using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Dtos;
using StarWars.Core.Models;
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
			var character = await Task.Run(() => new Character
			{
				Id = id
			});

			if (character == null) return NotFound(CHARACTER_CANNOT_BE_FOUND_MESSAGE);

			var characterDto = _mapper.Map<CharacterViewDto>(character);

			return Ok(characterDto);
		}

		public CharacterController(IMapper mapper)
		{
			_mapper = mapper;
		}

		private readonly IMapper _mapper;
		private const string CHARACTER_CANNOT_BE_FOUND_MESSAGE = "The character with given id cannot be found in database.";
	}
}

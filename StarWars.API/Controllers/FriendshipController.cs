using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Dtos;
using StarWars.API.Filters;
using StarWars.Core.Interfaces;
using System.Threading.Tasks;

namespace StarWars.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendshipController : ControllerBase
    {
        [HttpPost]
        [ValidateModelFilter]
        public async Task<IActionResult> MakeFriendship(MakeFriendshipDto dto)
        {
            var firstModel = await _characterService.GetByIdAsync(dto.FirstCharacterId);

            if (firstModel == null) return NotFound($"Character with id: {dto.FirstCharacterId} cannot be find in a database.");

            var secondModel = await _characterService.GetByIdAsync(dto.SecondCharacterId);

            if (secondModel == null) return NotFound($"Character with id: {dto.SecondCharacterId} cannot be find in a database.");

            var createdFriendship = await _friendshipService.MakeFriendship(firstModel, secondModel);

            return Created(string.Empty, _mapper.Map<FriendshipInfoDto>(createdFriendship));
        }

        [HttpDelete]
        [Route("{firstCharacterId}/{secondCharacterId}")]
        public async Task<IActionResult> RemoveFriendship(int firstCharacterId, int secondCharacterId)
        {
            await _friendshipService.RemoveFriendship(firstCharacterId, secondCharacterId);

            return Ok($"Friendship with id: [{firstCharacterId},{secondCharacterId}] has been deleted successfully.");
        }

        public FriendshipController(ICharacterService characterService, IFriendshipService friendshipService, IMapper mapper)
        {
            _characterService = characterService;
            _friendshipService = friendshipService;
            _mapper = mapper;
        }

        private readonly ICharacterService _characterService;
        private readonly IFriendshipService _friendshipService;
        private readonly IMapper _mapper;
    }
}

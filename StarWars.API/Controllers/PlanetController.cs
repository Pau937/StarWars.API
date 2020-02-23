using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWars.API.Dtos;
using StarWars.API.Filters;
using StarWars.Core.Interfaces;
using StarWars.Core.Models;

namespace StarWars.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetController : ControllerBase
    {
        [HttpPost]
        [ValidateModelFilter]
        public async Task<IActionResult> Add(NewPlanetDto dto)
        {
            Planet model = new Planet();

            _mapper.Map(dto, model);

            var createdPlanet = await _planetService.AddAsync(model);

            return Created($"localhost:5000/api/planet/{createdPlanet.Id}", _mapper.Map<PlanetDto>(createdPlanet));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanet(int id)
        {
            var planet = await _planetService.GetByIdAsync(id);

            if (planet == null) return NotFound("Planet with the given id cannot be found in a database");

            await _planetService.RemoveAsync(planet);

            return Ok($"Planet with id: {id} has been deleted successfully.");
        }

        [HttpPost]
        [Route("AssignPlanetToCharacter/{characterId}/{planetId}")]
        public async Task<IActionResult> AssignPlanetToCharacter(int characterId, int planetId)
        {
            var character = await _characterService.GetByIdAsync(characterId);

            if (character == null) return NotFound(CHARACTER_CANNOT_BE_FOUND_MESSAGE);

            var planet = await _planetService.GetByIdAsync(planetId);

            if (planet == null) return NotFound($"The planet with id: {planetId} cannot be found in database.");

            await _characterService.AddPlanetToCharacter(character, planet);

            return Ok($"Planet with id: {planetId} has been added successfully to character with id: {characterId}.");
        }

        [HttpDelete]
        [Route("RemovePlanetFromCharacter/{characterId}")]
        public async Task<IActionResult> RemovePlanetFromCharacter(int characterId)
        {
            var character = await _characterService.GetByIdAsync(characterId);

            if (character == null) return NotFound(CHARACTER_CANNOT_BE_FOUND_MESSAGE);

            if (character.Planet == null) return NotFound("Character with the given id have no planet assigned.");

            var planetId = character.Planet.Id;

            await _characterService.RemovePlanetFromCharacter(character);

            return Ok($"Planet with id: {planetId} has been removed successfully from character with id: {characterId}.");
        }

        public PlanetController(IMapper mapper, IPlanetService service, ICharacterService characterService)
        {
            _planetService = service;
            _mapper = mapper;
            _characterService = characterService;
        }

        private readonly IMapper _mapper;
        private readonly IPlanetService _planetService;
        private readonly ICharacterService _characterService;
        private const string CHARACTER_CANNOT_BE_FOUND_MESSAGE = "The character with given id cannot be found in database.";
    }
}

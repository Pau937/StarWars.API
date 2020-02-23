using AutoMapper;
using StarWars.API.Dtos;
using StarWars.Core.Models;
using System.Linq;

namespace StarWars.API.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Character, CharacterViewDto>().ForMember(x => x.Episodes, opt => opt.MapFrom(z => z.Appearances.Select(a => a.Episode)));
			CreateMap<Character, CharacterInfoDto>();
			CreateMap<CharacterDto, Character>();

			CreateMap<NewPlanetDto, Planet>();
			CreateMap<Planet, PlanetDto>();

			CreateMap<NewEpisodeDto, Episode>();
			CreateMap<Episode, EpisodeInfoDto>();
			CreateMap<Episode, AppearanceCharacterDto>();
		}
	}
}

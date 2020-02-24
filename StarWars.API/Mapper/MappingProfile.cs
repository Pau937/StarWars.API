using AutoMapper;
using StarWars.API.Dtos;
using StarWars.API.Dtos.Authentication;
using StarWars.Core.Models;
using StarWars.Core.Models.Authentication;
using System.Linq;

namespace StarWars.API.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Character, CharacterViewDto>()
				.ForMember(x => x.Friends, opt => opt.MapFrom(z => z.Friends.Select(a => a.Friend.Id == z.Id ? a.Character : a.Friend)))
				.ForMember(x => x.Episodes, opt => opt.MapFrom(z => z.Appearances.Select(a => a.Episode)));
			CreateMap<Character, CharacterInfoDto>();
			CreateMap<CharacterDto, Character>();

			CreateMap<NewPlanetDto, Planet>();
			CreateMap<Planet, PlanetDto>();

			CreateMap<NewEpisodeDto, Episode>();
			CreateMap<Episode, EpisodeInfoDto>();
			CreateMap<Episode, AppearanceCharacterDto>();

			CreateMap<Friendship, FriendshipInfoDto>();
			CreateMap<Character, FriendDto>();

			CreateMap<User, UserInfoDto>();
		}
	}
}

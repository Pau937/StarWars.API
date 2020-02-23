using AutoMapper;
using StarWars.API.Dtos;
using StarWars.Core.Models;

namespace StarWars.API.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Character, CharacterViewDto>();
		}
	}
}

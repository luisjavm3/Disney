using AutoMapper;
using Disney.DTOs.Character;
using Disney.Entities;

namespace Disney
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterResponseDto>();
        }
    }
}
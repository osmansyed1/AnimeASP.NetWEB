using AnimeWebApi.Dto;
using AnimeWebApi.Models;
using AutoMapper;

namespace AnimeWebApi.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, CharacterDto>();
            CreateMap<CharacterDto,Character>(); 
            CreateMap<Anime,AnimeDto>(); 
            CreateMap<AnimeDto,Anime>();
            CreateMap<Director, DirectorDto>();
            CreateMap<DirectorDto, Director>(); 
            CreateMap<Viewer, ViewerDto>();
            CreateMap<ViewerDto, Viewer>(); 
        }
    }
}

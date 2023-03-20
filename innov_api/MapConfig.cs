using AutoMapper;
using innov_api.Models;
using innov_api.Models.DTOs;

namespace innov_api
{
    public class MapConfig:Profile
    {
        public MapConfig()
        {
            CreateMap<Group, GroupDto>().ReverseMap();

            CreateMap<Verb, VerbDto>().ReverseMap();

            CreateMap<Verb, GroupVerbsDto>()
             .ReverseMap();
            CreateMap<Verb, VerbCreateDto>().ReverseMap();
            CreateMap<Paramter, ParamtersDto>().ReverseMap();


        }
    }
}

using AutoMapper;
using DrinkingGame.API.Models.Domain;
using DrinkingGame.API.Models.DTOs;

namespace DrinkingGame.API.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<UserDto, User>()
            //.ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
            .ReverseMap();
        CreateMap<AddUserRequestDto, User>()
            .ReverseMap();        
        CreateMap<UpdateUserRequestDto, User>()
            .ReverseMap();
        CreateMap<AddGameRequestDto, Game>()
            .ReverseMap();
        CreateMap<GameDto, Game>()
            .ReverseMap();
    }
}
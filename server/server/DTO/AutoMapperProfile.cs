using GuessingGame.DTO.responses;
using GuessingGame.models;
using AutoMapper;

namespace GuessingGame.DTO;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Player, PlayerPublicDTO>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.DisplayName)
            );
        CreateMap<Room, RoomDTO>();
        CreateMap<Player, LobbyDTO>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.DisplayName)
            );
    }
}
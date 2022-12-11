using GuessingGame.DTO.responses;
using GuessingGame.models;
using AutoMapper;
using GuessingGame.Models;

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
        CreateMap<Player, LoginWithRoomDTO>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.DisplayName)
            );

        CreateMap<Answer, AnswerDTO>();
        CreateMap<Question, QuestionDTO>();
        CreateMap<Administrator, AdministratorDTO>();
        CreateMap<GuessingGameModel, GameDTO>();
        CreateMap<GuessingGameModel, RoundStartDto>();
    }
}
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
        CreateMap<Question, QuestionNoAnswersDTO>();
        CreateMap<Administrator, AdministratorDTO>();
        CreateMap<GuessingGameModel, GameDTO>()
            .ForMember(
                dest => dest.CurrentQuestion,
                opt => opt.MapFrom(src => src.CurrentQuizGameQuestion != null ? src.CurrentQuizGameQuestion.Question : null)
            );
        CreateMap<GuessingGameModel, RoundStartDto>()
            .ForMember(
                dest => dest.CurrentQuestion,
                opt => opt.MapFrom(src => src.CurrentQuizGameQuestion != null ? src.CurrentQuizGameQuestion.Question : null)
            );
    }
}
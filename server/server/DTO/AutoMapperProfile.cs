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
                opt => opt.MapFrom(src =>
                    src.CurrentQuizGameQuestion != null ? src.CurrentQuizGameQuestion.Question : null)
            )
            .ForMember(
                dest => dest.PlayerAnswers,
                opt => opt.MapFrom(src =>
                    src.CurrentQuizGameQuestion != null ? src.CurrentQuizGameQuestion.Answers : null)
            );
        CreateMap<GuessingGameModel, RoundStartDto>()
            .ForMember(
                dest => dest.CurrentQuestion,
                opt => opt.MapFrom(src => src.CurrentQuizGameQuestion != null ? src.CurrentQuizGameQuestion.Question : null)
            );
        CreateMap<PlayerAnswer, PlayerAnswerDTO>()
            .ForMember(
                dest => dest.Question,
                opt => opt.MapFrom(src => src.QuizGameQuestion.Question)
            )
            .ForMember(
                dest => dest.GameQuestionId,
                opt => opt.MapFrom(src => src.QuizGameQuestion.Id)
            );
        CreateMap<ScoreboardLine, ScoreboardLineDTO>();
        CreateMap<Scoreboard, ScoreboardDTO>();
        
        // order by score
        // .ForMember(
        //     dest => dest.Scores,
        //     opt => opt.MapFrom(src => src.Scores.OrderByDescending(s => s.Score))
        // );
    }
}
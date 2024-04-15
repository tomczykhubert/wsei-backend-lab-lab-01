using AutoMapper;
using BackendLab01;
using Infrastructure.EF.Entities;

namespace Infrastructure.Mappers;

public class AutoEntityMapper: Profile
{
    public AutoEntityMapper()
    {
        //from db
        CreateMap<QuizEntity, Quiz>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Items, opt => opt.MapFrom<ISet<QuizItemEntity>>(src => src.Items));

        CreateMap<QuizItemEntity, QuizItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
            .ForMember(dest => dest.IncorrectAnswers, opt => opt.MapFrom<ISet<QuizItemAnswerEntity>>(src => src.IncorrectAnswers))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question));

        CreateMap<ISet<object>, List<object>>()
            .ConvertUsing(src => src.Select(e => e).ToList());

        CreateMap<QuizItemAnswerEntity, string>()
            .ConvertUsing(src => src.Answer);

        CreateMap<QuizItemUserAnswerEntity, QuizItemUserAnswer>()
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.UserAnswer))
            .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.QuizItem, opt => opt.MapFrom(src => src.QuizItem));


        //to db
        CreateMap<Quiz, QuizEntity>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Items, opt => opt.MapFrom<List<QuizItem>>(src => src.Items));

        CreateMap<QuizItem, QuizItemEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
            .ForMember(dest => dest.IncorrectAnswers, opt => opt.MapFrom<List<string>>(src => src.IncorrectAnswers))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question));

        CreateMap<List<object>, ISet<object>>()
            .ConvertUsing(src => new HashSet<object>(src));

        CreateMap<string, QuizItemAnswerEntity > ()
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src));

        CreateMap<QuizItemUserAnswer, QuizItemUserAnswerEntity>()
            .ForMember(dest => dest.UserAnswer, opt => opt.MapFrom(src => src.Answer))
            .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.QuizItem, opt => opt.MapFrom(src => src.QuizItem));
    }
}
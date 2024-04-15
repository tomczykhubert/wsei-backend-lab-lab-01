using AutoMapper;
using BackendLab01;
using Infrastructure.EF.Entities;

namespace Infrastructure.Mappers;

public class AutoMapper: Profile
{
    public AutoMapper()
    {
        CreateMap<QuizEntity, Quiz>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Items, opt => opt.MapFrom<ISet<QuizItemEntity>>(src => src.Items));
        CreateMap<QuizItemEntity, QuizItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
            .ForMember(dest => dest.IncorrectAnswers, opt => opt.MapFrom(src => src.IncorrectAnswers))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question));
        CreateMap<QuizItemAnswerEntity, string>()
            .ForMember(dest => dest, opt => opt.MapFrom(src => src.Answer));
        //CreateMap<ISet<QuizItemAnswerEntity>, List<string>>().ForMember()
    }
}
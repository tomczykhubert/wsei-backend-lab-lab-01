using AutoMapper;
using BackendLab01;
using WebApi.DTO;

namespace WebApi.Mapper;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<QuizItem, QuizItemDto>()
            .ForMember(
                q => q.Options,
                op => op.MapFrom(i => new List<string>(i.IncorrectAnswers) { i.CorrectAnswer }));
        CreateMap<Quiz, QuizDto>()
            .ForMember(
                q => q.Items,
                op => op.MapFrom<List<QuizItem>>(i => i.Items)
            );
        CreateMap<NewQuizDto, Quiz>().AfterMap((src, dest) =>
        {
            src.Title = dest.Title;
        });
        //CreateMap<List<QuizItemUserAnswer>, QuizSummaryDto>().AfterMap((src, dest) =>
        //{
        //  dest.UserAnswers = 
        //});
    }
}
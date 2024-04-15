using BackendLab01;
using WebApi.DTO;

namespace WebApi.Mapper;

public class QuizMapper
{
    public static QuizDto MapQuizDto(Quiz quiz)
    {
        return new QuizDto()
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Items = quiz.Items.Select(QuizItemMapper.MapItemDto).ToList()
        };
    }
}
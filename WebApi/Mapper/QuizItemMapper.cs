using BackendLab01;
using WebApi.DTO;

namespace WebApi.Mapper;

public class QuizItemMapper
{
    public static QuizItemDto MapItemDto(QuizItem quizItem)
    {
        return new QuizItemDto()
        {
            Id = quizItem.Id,
            Question = quizItem.Question,
            Options = new List<string>(quizItem.IncorrectAnswers)
            {
                quizItem.CorrectAnswer,
            }
        };
    }
}
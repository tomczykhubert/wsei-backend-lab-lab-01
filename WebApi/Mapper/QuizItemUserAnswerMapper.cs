using BackendLab01;
using WebApi.DTO;

namespace WebApi.Mapper;

public class QuizItemUserAnswerMapper
{
    public static QuizItemUserAnswerDto MapQuizItemUserAnswerDto(QuizItemUserAnswer quizItemUserAnswer)
    {
        return new QuizItemUserAnswerDto()
        {
            Answer = quizItemUserAnswer.Answer,
            quizItemId = quizItemUserAnswer.QuizItem.Id
        };
    }
}
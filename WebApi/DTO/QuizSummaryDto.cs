namespace WebApi.DTO;

public class QuizSummaryDto
{
    public int UserId { get; set; }
    public QuizDto Quiz { get; set; }
    public List<QuizItemUserAnswerDto> UserAnswers { get; set;  }
    public int CorrectTotal { get; set; }
}
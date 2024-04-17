using BackendLab01;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Mapper;

namespace WebApi.Controller;

[Route("/api/v1/user/quizzes")]
public class WebApiUserQuiz : ControllerBase
{
    private readonly IQuizUserService _service;

    public WebApiUserQuiz(IQuizUserService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IEnumerable<QuizDto> GetAllQuizes()
    {
        return _service.FindAllQuizes().Select(QuizMapper.MapQuizDto);
    }
    
    [Route("{id}")]
    [HttpGet]
    public ActionResult<QuizDto?> GetOneQuiz(int id)
    {
        Quiz? result = _service.FindQuizById(id);
        return result == null ? NotFound() : QuizMapper.MapQuizDto(result);
    }

    [HttpPost]
    [Authorize(Policy = "Bearer")]
    [Route("{quizId}/items/{itemId}/answers")]
    public ActionResult SaveUserAnswer([FromBody] SaveAnswerDto saveAnswerDto, int quizId, int itemId)
    {
        try
        {
            var item = _service.SaveUserAnswerForQuiz(quizId, saveAnswerDto.UserId, itemId, saveAnswerDto.Answer);
            return Created("uri to answer", null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{quizId}/answers/{userId}")]
    public ActionResult<List<QuizItemUserAnswer>> GetUserAnswers(int quizId, int userId)
    {
        try
        {
            var result = _service.GetUserAnswersForQuiz(quizId, userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{quizId}/summary/{userId}")]
    public ActionResult<QuizSummaryDto?> GetQuizSummaryForUser(int quizId, int userId)
    {
        Quiz? quiz = _service.FindQuizById(quizId);
        if (quiz == null) return NotFound();
        
        var answers = _service.GetUserAnswersForQuiz(quizId, userId);

        return new QuizSummaryDto()
        {
            Quiz = QuizMapper.MapQuizDto(quiz),
            UserAnswers = answers.Select(QuizItemUserAnswerMapper.MapQuizItemUserAnswerDto).ToList(),
            CorrectTotal = _service.CountCorrectAnswersForQuizFilledByUser(quizId, userId),
            UserId = userId
        };
    }
}
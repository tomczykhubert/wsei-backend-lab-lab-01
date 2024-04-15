using ApplicationCore.Interfaces.AdminService;
using AutoMapper;
using BackendLab01;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.DTO;

namespace WebApi.Controller;

[ApiController]
[Route("/api/v1/admin/quizzes")]
public class ApiQuizAdminController : ControllerBase
{
    private readonly IQuizAdminService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<QuizItem> _validator;

    public ApiQuizAdminController(IQuizAdminService service, IMapper mapper, IValidator<QuizItem> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpPost]
    public IActionResult CreateQuiz(LinkGenerator link,  NewQuizDto dto)
    {
        var quiz = _service.AddQuiz(_mapper.Map<Quiz>(dto));
        return Created(
            link.GetUriByAction(HttpContext,nameof(GetById), null, new {id = quiz.Id}),
            quiz
        );
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Quiz> GetById(int id)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == id);
        return quiz == null ? NotFound() : Ok(quiz);
    }
    
    [HttpPatch]
    [Route("{quizId}")]
    [Consumes("application/json-patch+json")]
    public ActionResult<Quiz> EditQuiz(int quizId, JsonPatchDocument<Quiz>? patchDoc)
    {
        var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
        quiz = JsonConvert.DeserializeObject<Quiz>(JsonConvert.SerializeObject(quiz));
        if (quiz is null || patchDoc is null)
        {
            return NotFound(new
            {
                error = $"Quiz with id {quizId} not found"
            });
        }
        
        if (_service.isQuizAnswered(quizId))
        {
            return BadRequest(new
            {
                error = "Can't edit quiz that is already answered!"
            });
        }
        
        var disabledOperation =
            patchDoc.Operations.FirstOrDefault(p => p.OperationType != OperationType.Replace && p.path == "id");
        if (disabledOperation is not null)
            return BadRequest(new
            {
                error = "Can't replace id!"
            });
        
        patchDoc.ApplyTo(quiz, ModelState);
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        foreach (var item in quiz.Items)
        {
            var validationResult = _validator.Validate(item);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
        }
        
        var items = quiz.Items;
        quiz.Items = new List<QuizItem>();
        
        _service.UpdateQuiz(quiz);

        foreach (var item in items)
        {
            _service.RemoveItemById(item.Id);
            _service.AddQuizItemToQuiz(quizId, item);
        }
        
        return Ok(_service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId));
    }
}
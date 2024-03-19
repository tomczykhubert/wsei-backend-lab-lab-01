
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{
    private readonly IQuizUserService _userService;

    private readonly ILogger _logger;
    
    public Summary(IQuizUserService userService, ILogger<QuizModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [BindProperty] 
    public int CorrectAnswers { get; set; }
    [BindProperty] 
    public int NumberOfQuestions { get; set; }

    [BindProperty]
    public int QuizId { get; set; }

    public void OnGet(int quizId)
    {
        QuizId = quizId;
        var quiz = _userService.FindQuizById(QuizId);
        if (quiz is not null)
        {
            NumberOfQuestions = quiz.Items.Count;

        }
        CorrectAnswers = _userService.CountCorrectAnswersForQuizFilledByUser(QuizId, 1);
    }
}
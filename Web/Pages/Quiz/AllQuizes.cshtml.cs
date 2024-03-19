using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
namespace BackendLab01.Pages;

public class AllQuizesModel : PageModel
{
    private readonly IQuizUserService _userService;

    private readonly ILogger _logger;
    
    [BindProperty]
    public List<BackendLab01.Quiz> Quizes { get; set; }
    
    public AllQuizesModel(IQuizUserService userService, ILogger<QuizModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    public void OnGet()
    {
        Quizes = _userService.FindAllQuizes();
    }

    public IActionResult OnPost(int quiz)
    {
        return RedirectToPage("Item", new {quizId = quiz, itemId = 1});
    }
}
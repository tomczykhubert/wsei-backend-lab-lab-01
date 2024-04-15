using BackendLab01;

namespace WebApi.DTO;

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<QuizItemDto> Items { get; set; }
}
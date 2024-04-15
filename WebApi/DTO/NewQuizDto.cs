using System.ComponentModel.DataAnnotations;
namespace WebApi.DTO;

public class NewQuizDto
{
    [Required]
    [Length(minimumLength: 3, maximumLength: 200)]
    public string Title { get; set; }
}
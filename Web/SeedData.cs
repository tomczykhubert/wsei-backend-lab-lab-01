using ApplicationCore.Interfaces.Criteria;
using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
            var item1 = quizItemRepo.Add(new QuizItem(1, "Ile to jest 2+2?", new List<string>() { "1", "2", "3" }, "4"));
            var item2 = quizItemRepo.Add(new QuizItem(2, "Ile to jest 7*3?", new List<string>() { "20", "12", "49" }, "21"));
            var item3 = quizItemRepo.Add(new QuizItem(3, "Ile to jest 10/2?", new List<string>() { "4", "7", "2" }, "5"));
            quizRepo.Add(new Quiz(1, new List<QuizItem>(){item1, item2, item3}, "Matematyka"));
            
            item1 = quizItemRepo.Add(new QuizItem(4, "Językiem programowania jest:", new List<string>() { "HTML", "CSS", "JSON" }, "C#"));
            item2 = quizItemRepo.Add(new QuizItem(5, "Pytanie 2", new List<string>() { "Answer 1", "Answer 2", "Answer 3" }, "Correct answer"));
            item3 = quizItemRepo.Add(new QuizItem(6, "Pytanie 3", new List<string>() { "Answer 1", "Answer 2", "Answer 3" }, "Correct answer"));
            quizRepo.Add(new Quiz(2, new List<QuizItem>(){item1, item2, item3}, "Informatyka"));
        }
    }
}
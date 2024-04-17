using AutoMapper;
using BackendLab01;
using Infrastructure.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class QuizUserServiceEF : IQuizUserService
    {
        private readonly QuizDbContext _context;
        private IMapper _mapper;

        public QuizUserServiceEF(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Quiz CreateAndGetQuizRandom(int count)
        {
            throw new NotImplementedException();
        }

        public List<Quiz> FindAllQuizes()
        {
            return _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .Select(_mapper.Map<Quiz>)
                .ToList();
        }

        public Quiz? FindQuizById(int id)
        {
            var entity = _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .FirstOrDefault(e => e.Id == id);
            return entity is null ? null : _mapper.Map<Quiz>(entity);
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            var quizzEntity = _context.Quizzes.AsNoTracking().FirstOrDefault(e => e.Id == quizId);
            if (quizzEntity == null)
                throw new QuizNotFoundException($"Quiz with id {quizId} not found");
            return _context.UserAnswers.Include(a => a.QuizItem).ThenInclude(i => i.IncorrectAnswers).Where(a => a.UserId == userId && a.QuizId == quizId).Select(_mapper.Map<QuizItemUserAnswer>).ToList();
        }

        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
        {
            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                UserId = userId,
                QuizItemId = quizItemId,
                QuizId = quizId,
                UserAnswer = answer
            };
            try
            {
                var saved = _context.UserAnswers.Add(entity).Entity;
                _context.SaveChanges();
                return _mapper.Map<QuizItemUserAnswer>(saved);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.StartsWith("The INSERT"))
                {
                    throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
                }
                if (e.InnerException.Message.StartsWith("Violation of"))
                {
                    throw new QuizAnswerItemAlreadyExistsException(quizId, quizItemId, userId);
                }
                throw new Exception(e.Message);
            }
        }
    }
}

using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;

namespace AIDentify.Repositry
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {
        private readonly ContextAIDentify _context;

        public QuizAttemptRepository(ContextAIDentify context)
        {
            _context = context;
        }

        #region GetAll

        public List<QuizAttempt> GetAll()
        {
            return _context.QuizAttempt.ToList();
        }

        #endregion

        #region Get by Quiz Id

        public QuizAttempt GetById(string id)
        {
            return _context.QuizAttempt.FirstOrDefault(q => q.Id == id);
        }

        #endregion

        #region Add

        public void Add(QuizAttempt quizAttempt)
        {
            _context.QuizAttempt.Add(quizAttempt);
            _context.SaveChanges();
        }

        #endregion

        #region Update

        public void Update(QuizAttempt quizAttempt)
        {
            var existingQuizAttempt = _context.QuizAttempt.FirstOrDefault(q => q.Id == quizAttempt.Id);
            if (existingQuizAttempt != null)
            {
                _context.QuizAttempt.Update(quizAttempt);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Delete
        public void Delete(QuizAttempt quizAttempt)
        {
            var existingQuizAttempt = _context.QuizAttempt.FirstOrDefault(q => q.Id == quizAttempt.Id);
            if (existingQuizAttempt != null)
            {
                _context.QuizAttempt.Remove(quizAttempt);
                _context.SaveChanges();
            }
        }

        #endregion

        #region DeleteAll

        public void DeleteAll()
        {
            var quizAttempts = _context.QuizAttempt.ToList();
            _context.QuizAttempt.RemoveRange(quizAttempts);
            _context.SaveChanges();
        }

        #endregion
    }
}

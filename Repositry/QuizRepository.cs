using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;

namespace AIDentify.Repositry
{
    public class QuizRepository: IQuizRepository
    {
        private readonly ContextAIDentify _context;

        public QuizRepository(ContextAIDentify context)
        {
            _context = context;
        }

        #region GetAll

        public List<Quiz> GetAll()
        {
            return _context.Quiz.ToList();
        }

        #endregion

        #region Get by Quiz Id

        public Quiz GetById(string id)
        {
            return _context.Quiz.FirstOrDefault(q => q.Id == id);
        }

        #endregion

        #region Add

        public void Add(Quiz quiz)
        {
            _context.Quiz.Add(quiz);
            _context.SaveChanges();
        }

        #endregion

        #region Update

        public void Update(Quiz quiz)
        {
            var existingQuiz = _context.Quiz.FirstOrDefault(q => q.Id == quiz.Id);
            if (existingQuiz != null)
            {
                _context.Quiz.Update(quiz);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Delete

        public void Delete(Quiz quiz)
        {
            var existingQuiz = _context.Quiz.FirstOrDefault(q => q.Id == quiz.Id);
            if (existingQuiz != null)
            {
                _context.Quiz.Remove(quiz);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Delete All

        public void DeleteAll()
        {
            var quizzes = _context.Quiz.ToList();
            _context.Quiz.RemoveRange(quizzes);
            _context.SaveChanges();
        }

        #endregion
    }
}

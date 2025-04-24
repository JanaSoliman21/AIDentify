using AIDentify.Models;

namespace AIDentify.IRepositry
{
    public interface IQuizAttemptRepository
    {
        List<QuizAttempt> GetAll();
        QuizAttempt GetById(string id);
        void Add(QuizAttempt quizAttempt);
        void Update(QuizAttempt quizAttempt);
        void Delete(QuizAttempt quizAttempt);
        void DeleteAll();
    }
}

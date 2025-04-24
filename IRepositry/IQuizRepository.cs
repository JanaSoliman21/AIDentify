using AIDentify.Models;

namespace AIDentify.IRepositry
{
    public interface IQuizRepository
    {
        List<Quiz> GetAll();
        Quiz GetById(string id);
        void Add(Quiz quiz);
        void Update(Quiz quiz);
        void Delete(Quiz quiz);
        void DeleteAll();
    }
}

using AIDentify.Models;


namespace AIDentify.IRepositry
{
    public interface IUserRepositry
    {
        List<User> GetAll();
        void Add(User user);
    }
}

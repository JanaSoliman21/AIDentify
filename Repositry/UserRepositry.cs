using AIDentify.IRepositry;
using AIDentify.Models.Context;
using AIDentify.Models;

namespace AIDentify.Repositry
{
    public class UserRepositry : IUserRepositry
    {
        ContextAIDentify context;
        
        public UserRepositry(ContextAIDentify _context)
        {
            context = _context;
            
        }
        public List<User> GetAll()
        {
            var User = context.User.ToList();
            return User;
        }

        public void Add(User user)
        {
            //var pro = new Product();
            //pro.Name = product.Name;
            //pro.Description= product.Description;
            //pro.Price = product.Price;
            context.User.Add(user);
            context.SaveChanges();
        }
    }
}

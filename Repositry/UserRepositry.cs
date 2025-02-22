using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using Microsoft.AspNetCore.Identity;

namespace AIDentify.Repositry
{
    public class UserRepositry:IUserRepositry
    {
        
        private readonly ContextAIDentify context;
        public UserRepositry(ContextAIDentify context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            this.context = context;
        }
        public List<Doctor> GetAllDoctors()
        {
            var Doctors = context.Doctor.ToList();
            return Doctors;
        }
        public List<Student> GetAllStudents()
        {
            var Students = context.Student.ToList();
            return Students;
        }
        public List<Admin> GetAllAdmins()
        {
            var Admins = context.Admin.ToList();
            return Admins;
        }



    }
}

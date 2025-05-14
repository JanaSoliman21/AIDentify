using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;

namespace AIDentify.Repositry
{
    public class UserRepositry:IUserRepositry
    {
        
        private readonly ContextAIDentify context;
        public UserRepositry(ContextAIDentify context)
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
        public async Task AddAsAdminAsync(ApplicationUser user)
        {
            var admin = new Admin
            { 
              Admin_ID = user.Id,
              FirstName = user.FirstName,
              LastName = user.LastName,
              UserName = user.UserName,
              Email = user.Email,
              Password = user.PasswordHash
            };
            context.Admin.Add(admin);   
            await context.SaveChangesAsync();

        }

        public async Task AddAsDoctorAsync(ApplicationUser user)
        {
            var doctor = new Doctor
            {
                Doctor_ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
                ClinicName = user.ClinicName
            };
            context.Doctor.Add(doctor);
            await context.SaveChangesAsync();

        }

        public async Task AddAsStudentAsync(ApplicationUser user)
        {
            var student = new Student
            {
                Student_ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
                University = user.University,
                Level = (int)user.Level

            };
            context.Student.Add(student);
            await context.SaveChangesAsync();

        }





    }
}

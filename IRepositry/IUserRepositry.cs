using AIDentify.Models;


namespace AIDentify.IRepositry
{
    public interface IUserRepositry
    {
        List<Doctor> GetAllDoctors();
        List<Student> GetAllStudents();
        List<Admin> GetAllAdmins();

    }
}

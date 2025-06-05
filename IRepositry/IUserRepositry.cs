using AIDentify.Models;


namespace AIDentify.IRepositry
{
    public interface IUserRepositry
    {
        List<Doctor> GetAllDoctors();
        List<Student> GetAllStudents();
        List<Admin> GetAllAdmins();
        Task AddAsAdminAsync(ApplicationUser user);
        Task AddAsDoctorAsync(ApplicationUser user);
        Task AddAsStudentAsync(ApplicationUser user);
        Task<Admin?> GetByIdAdminAsync(string id);
        Task<Doctor?> GetByIdDoctorAsync(string id);
        Task<Student?> GetByIdStudentAsync(string id);
        Task DeleteAdmin(string id);
        Task DeleteStudent(string id);
        Task DeleteDoctor(string id);


    }
}

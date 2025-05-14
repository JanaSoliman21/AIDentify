using AIDentify.Models;

namespace AIDentify.IRepositry
{
    public interface IIdentityRepo
    {
        Task<(bool Success, List<string> Errors, ApplicationUser? User)> RegisterAsAdminAsync(Admin user);
        Task<(bool Success, List<string> Errors, ApplicationUser? User)> RegisterAsDoctorAsync(Doctor user);
        Task<(bool Success, List<string> Errors, ApplicationUser? User)> RegisterAsStudentAsync(Student user);
        Task<(bool Success, string Token, List<string> Errors, List<string> Roles)> LoginAsync(string Username, String password);
        Task<(bool Success, List<string> Errors)> LogoutAsync(string token);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<string> GetNewTokenAsync(ApplicationUser user);
    }
}

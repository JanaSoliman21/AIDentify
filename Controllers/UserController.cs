using AIDentify.DTO;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        IUserRepositry userA;
        private readonly ContextAIDentify context;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager ,IUserRepositry userA, ContextAIDentify context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userA = userA;
            this.context = context;
            
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctor()
        {

            var doctors = userA.GetAllDoctors();
            return Ok(doctors);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudent()
        {

            var student = userA.GetAllStudents();
            return Ok(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {

            var admin = userA.GetAllAdmins();
            return Ok(admin);
        }

        [Authorize]
        [HttpGet("UserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new { message = "User is not logged in" });

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(); 

            if (role == "Doctor")
            {
                return Ok(new
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    ClinicName = user.ClinicName,
                    Role = role
                });
            }
            else if (role == "Student")
            {
                return Ok(new
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    University = user.University,
                    Level = user.Level,
                    Role = role
                });
            }
            else if (role == "Admin")
            {
                return Ok(new
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role


                });
            }

            return BadRequest(new { message = "Role not recognized" });
        }

        [Authorize]
        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto profile)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new { message = "User is not logged in" });

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(); // Assuming one role per user

            // Logging for Debugging
            Console.WriteLine($"Updating User: {user.Id}, Role: {role}");

            // Ensure fields are only updated if they are non-empty
            user.FirstName = !string.IsNullOrWhiteSpace(profile.FirstName) ? profile.FirstName : user.FirstName;
            user.LastName = !string.IsNullOrWhiteSpace(profile.LastName) ? profile.LastName : user.LastName;
            user.UserName = !string.IsNullOrWhiteSpace(profile.UserName) ? profile.UserName : user.UserName;
            user.Email = !string.IsNullOrWhiteSpace(profile.Email) ? profile.Email : user.Email;

            if (role == "Doctor")
            {
                user.ClinicName = !string.IsNullOrWhiteSpace(profile.ClinicName) ? profile.ClinicName : user.ClinicName;

                var doctor = await context.Doctor.FirstOrDefaultAsync(d => d.Doctor_ID == user.Id);
                if (doctor != null)
                {
                    doctor.FirstName = user.FirstName;
                    doctor.LastName = user.LastName;
                    doctor.UserName = user.UserName;
                    doctor.Email = user.Email;
                    doctor.ClinicName = user.ClinicName;

                    context.Entry(doctor).State = EntityState.Modified; // Proper tracking
                }
            }
            else if (role == "Student")
            {
                user.University = !string.IsNullOrWhiteSpace(profile.University) ? profile.University : user.University;
                user.Level = profile.Level.HasValue ? profile.Level.Value : user.Level; // Proper handling for nullable int

                var student = await context.Student.FirstOrDefaultAsync(s => s.Student_ID == user.Id);
                if (student != null)
                {
                    student.FirstName = user.FirstName;
                    student.LastName = user.LastName;
                    student.UserName = user.UserName;
                    student.Email = user.Email;
                    student.University = user.University;
                    student.Level = user.Level ?? student.Level;

                    context.Entry(student).State = EntityState.Modified;
                }
            }
            else if (role == "Admin")
            {
                var admin = await context.Admin.FirstOrDefaultAsync(a => a.Admin_ID == user.Id);
                if (admin != null)
                {
                    admin.FirstName = user.FirstName;
                    admin.LastName = user.LastName;
                    admin.UserName = user.UserName;
                    admin.Email = user.Email;

                    context.Entry(admin).State = EntityState.Modified;
                }
            }
            else
            {
                return BadRequest(new { message = "Role not recognized" });
            }

            // ✅ Save changes
            await context.SaveChangesAsync();

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                Console.WriteLine("Profile updated successfully.");
                return Ok(new { message = "Profile updated successfully" });
            }

            return BadRequest(result.Errors);
        }

    }
}

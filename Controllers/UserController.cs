﻿using AIDentify.DTO;
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
        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctor()
        {

            var doctors = userA.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudent()
        {

            var student = userA.GetAllStudents();
            return Ok(student);
        }
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
            var role = roles.FirstOrDefault(); 

            user.FirstName = profile.FirstName ?? user.FirstName;
            user.LastName = profile.LastName ?? user.LastName;
            user.UserName = profile.UserName ?? user.UserName;
            user.Email = profile.Email ?? user.Email;

            if (role == "Doctor")
            {
                user.ClinicName = profile.ClinicName ?? user.ClinicName;

                var doctor = await context.Doctor.FirstOrDefaultAsync(d => d.Doctor_ID == user.Id);
                if (doctor != null)
                {
                    doctor.FirstName = user.FirstName;
                    doctor.LastName = user.LastName;
                    doctor.UserName = user.UserName;
                    doctor.Email = user.Email;
                    doctor.ClinicName = user.ClinicName;

                    context.Doctor.Update(doctor);
                }
            }
            else if (role == "Student")
            {
                user.University = profile.University ?? user.University;
                user.Level = profile.Level ?? user.Level;

                var student = await context.Student.FirstOrDefaultAsync(s => s.Student_ID == user.Id);
                if (student != null)
                {
                    student.FirstName = user.FirstName;
                    student.LastName = user.LastName;
                    student.UserName = user.UserName;
                    student.Email = user.Email;
                    student.University = user.University;
                    student.Level = (int)user.Level;

                    context.Student.Update(student);
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

                    context.Admin.Update(admin);
                }
            }
            else
            {
                return BadRequest(new { message = "Role not recognized" });
            }

            await context.SaveChangesAsync();

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok(new { message = "Profile updated successfully" });

            return BadRequest(result.Errors);
        }
    }
}

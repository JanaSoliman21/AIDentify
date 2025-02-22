using AIDentify.Models.Context;
using AIDentify.Models;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AIDentify.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ContextAIDentify context;
        internal static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ContextAIDentify context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            this.context = context;

        }
        [HttpPost("Register_Admin")]
        public async Task<IActionResult> RegisterNewAdmin(RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user1 = new ApplicationUser();
            {
                user1.FirstName = user.FirstName;
                user1.LastName = user.LastName;
                user1.UserName = user.Username;
                user1.Email = user.Email;

            }
            IdentityResult result = await _userManager.CreateAsync(user1, user.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(user1, "Admin");
            var admin = new Admin
            {
                Admin_ID = user1.Id,
                UserName = user1.UserName,
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                Email = user1.Email,
                Password = user1.PasswordHash
            };

            context.Admin.Add(admin);
            await context.SaveChangesAsync();
            return Ok("Admin registered successfully");
        }

        [HttpPost("Register_Doctor")]
        public async Task<IActionResult> RegisterDoctor(RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user1 = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Email = user.Email,
                ClinicName = user.ClinicName
            };

            IdentityResult result = await _userManager.CreateAsync(user1, user.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user1, "Doctor");


            var doctor = new Doctor
            {
                Doctor_ID = user1.Id,
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                UserName = user1.UserName,
                Email = user1.Email,
                Password = user1.PasswordHash,
                ClinicName = user1.ClinicName
            };

            context.Doctor.Add(doctor);
            await context.SaveChangesAsync();

            return Ok("Doctor registered successfully");
        }

        [HttpPost("Register_Student")]
        public async Task<IActionResult> RegisterNewStudent(RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser user1 = new ApplicationUser();
            {
                user1.FirstName = user.FirstName;
                user1.LastName = user.LastName;
                user1.UserName = user.Username;
                user1.Email = user.Email;
                user1.University = user.University;
                user1.Level = user.Level;
            }
            IdentityResult result = await _userManager.CreateAsync(user1, user.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(user1, "Student");
            var student = new Student
            {
                Student_ID = user1.Id,
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                UserName = user1.UserName,
                Email = user1.Email,
                Password = user1.PasswordHash,
                University = user1.University,
                Level = (int)user1.Level
            };

            context.Student.Add(student);
            await context.SaveChangesAsync();
            return Ok("Student registered successfully");


        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? user2 = await _userManager.FindByNameAsync(login.UserName);
                if (user2 != null)
                {
                    if (await _userManager.CheckPasswordAsync(user2, login.Password))
                    {

                        //token
                        var claims = new List<Claim>();
                        //claims.Add(new Claim("name","value"));
                        claims.Add(new Claim(ClaimTypes.Name, user2.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user2.Id));
                        claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await _userManager.GetRolesAsync(user2);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

                        }
                        //signingCredentials
                        var secretKey = _configuration["JWT:SecretKey"];
                        if (string.IsNullOrEmpty(secretKey))
                        {
                            throw new ArgumentNullException("JWT:SecretKey", "JWT SecretKey is missing in configuration.");
                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: sc

                            );
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,

                        };
                        return Ok(_token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {

                    ModelState.AddModelError("", "User Name is Invalid");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return BadRequest(new { message = "No token provided" });
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (expClaim == null)
            {
                return BadRequest(new { message = "Invalid token" });
            }

            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime;

            _blacklistedTokens[token] = expiryDate;

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgetPassDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"{Request.Scheme}://{Request.Host}/api/auth/reset-password?token={resetToken}&email={model.Email}";

            return Ok(new { message = "Password reset link generated", resetLink });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var resetResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!resetResult.Succeeded)
            {
                return BadRequest(new { message = "Password reset failed", errors = resetResult.Errors });
            }

            return Ok(new { message = "Password reset successfully" });
        }

        [HttpDelete("Delete_Account")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized(new { message = "User is not logged in" });

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == "Doctor")
            {
                var doctor = await context.Doctor.FirstOrDefaultAsync(d => d.Doctor_ID == user.Id);
                if (doctor != null)
                {
                    context.Doctor.Remove(doctor);
                }
            }
            else if (role == "Student")
            {
                var student = await context.Student.FirstOrDefaultAsync(s => s.Student_ID == user.Id);
                if (student != null)
                {
                    context.Student.Remove(student);
                }
            }
            else if (role == "Admin")
            {
                var admin = await context.Admin.FirstOrDefaultAsync(a => a.Admin_ID == user.Id);
                if (admin != null)
                {
                    context.Admin.Remove(admin);
                }
            }
            else
            {
                return BadRequest(new { message = "Role not recognized" });
            }

            await context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = $"{role} Account Deleted Successfully" });
            }

            return BadRequest(result.Errors);
        }


    }
}

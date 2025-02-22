
using System.Text.Json.Serialization;
using AIDentify.Extension;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using AIDentify.Repositry;
using Microsoft.AspNetCore.Identity;

//using AIDentify.Repositry;
using Microsoft.EntityFrameworkCore;

namespace AIDentify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ContextAIDentify>(
            OptionsBuilder =>
            {
              OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }

             );

            builder.Services.AddScoped<IUserRepositry, UserRepositry>();
            builder.Services.AddScoped<IPlanRepository, PlanRepositry>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;

                }).AddEntityFrameworkStores<ContextAIDentify>().AddDefaultTokenProviders();//Reset 
            builder.Services.AddCustomJwtAuth(builder.Configuration);


            var app = builder.Build();
            app.UseMiddleware<TokenBlacklistMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

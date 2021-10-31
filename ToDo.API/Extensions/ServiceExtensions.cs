using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ToDo.DataAccess;

namespace ToDo.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "ToDo list API",
                Description = "API for current application"
            }));
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = jwtSettings["key"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("issuer").Value,
                    ValidAudience = jwtSettings.GetSection("audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }
    }
}
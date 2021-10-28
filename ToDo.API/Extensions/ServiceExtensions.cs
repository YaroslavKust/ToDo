using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
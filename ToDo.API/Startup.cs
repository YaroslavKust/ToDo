using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ToDo.API.ActionFilters;
using ToDo.API.Extensions;
using ToDo.DataAccess;
using ToDo.DataAccess.Interfaces;


namespace ToDo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureDataAccess(Configuration);
            services.ConfigureSwagger();
            services.ConfigureAuthentication(Configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(Log.Logger);

            services.AddScoped<CheckEmployeeExistsAttribute>();
            services.AddScoped<CheckTaskExistsAttribute>();
            services.AddScoped<ValidateModelAttribute>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "ToDo list API v1"));

            app.ConfigureExceptionsHandler();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints=>endpoints.MapControllers());
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using ToDo.DataAccess.Interfaces;

namespace ToDo.API.ActionFilters
{
    public class CheckEmployeeExistsAttribute : IAsyncActionFilter
    {
        private IUnitOfWork _db;
        private ILogger _logger;

        public CheckEmployeeExistsAttribute(IUnitOfWork db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments["id"];
            var employee = await _db.Employees.GetEmployeeAsync(id);

            if (employee is null)
            {
                _logger.Information($"Employee with id {id} doesn't exists");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("Employee", employee);
                await next.Invoke();
            }
        }
    }
}
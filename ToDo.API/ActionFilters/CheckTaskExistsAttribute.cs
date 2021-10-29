using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Models;

namespace ToDo.API.ActionFilters
{
    public class CheckTaskExistsAttribute: IAsyncActionFilter
    {
        private IUnitOfWork _db;
        private ILogger _logger;

        public CheckTaskExistsAttribute(IUnitOfWork db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int employeeId = (int)context.ActionArguments["employeeId"];
            int id = (int)context.ActionArguments["id"];

            var employee = await _db.Employees.GetEmployeeAsync(employeeId);

            if (employee is null)
            {
                _logger.Information($"Employee with id {employeeId} doesn't exists");
                context.Result = new NotFoundResult();
            }
            else
            {
                var task = await _db.Tasks.GetTaskAsync(employeeId, id);

                if (task is null)
                {
                    _logger.Information($"Task with id {id} doesn't exists");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    context.HttpContext.Items.Add("Task", task);
                    await next.Invoke();
                }

            }
        }
    }
}
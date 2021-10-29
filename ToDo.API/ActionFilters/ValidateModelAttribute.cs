using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace ToDo.API.ActionFilters
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        private ILogger _logger;

        public ValidateModelAttribute(ILogger logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.Information("Model state is invalid");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
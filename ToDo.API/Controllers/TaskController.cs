using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ToDo.API.ActionFilters;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.DTO;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.Web.Controllers
{
    [Route("api/employees/{employeeId}/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IUnitOfWork _db;
        private IMapper _mapper;

        public TaskController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks(int employeeId, [FromQuery] TaskParameters parameters)
        {
            var tasks = await _db.Tasks.GetTasksAsync(employeeId, parameters);
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return Ok(tasksDto);
        }


        [HttpGet("{id}", Name = "TaskById")]
        [ServiceFilter(typeof(CheckTaskExistsAttribute))]
        public IActionResult GetTask(int employeeId, int id)
        {
            var task = HttpContext.Items["Task"] as MyTask;
            var taskDto = _mapper.Map<TaskDto>(task);

            return Ok(taskDto);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> CreateTask(int employeeId, [FromBody] TaskForCreate task)
        {
            var taskForDb = _mapper.Map<MyTask>(task);
            taskForDb.EmployeeId = employeeId;

            _db.Tasks.Create(taskForDb);
            await _db.SaveAsync();

            var result = _mapper.Map<TaskDto>(taskForDb);

            return CreatedAtRoute("TaskById", new { employeeId, id = taskForDb.Id }, result);
        }


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [ServiceFilter(typeof(CheckTaskExistsAttribute))]
        public async Task<IActionResult> UpdateTask(int employeeId, int id, [FromBody] TaskForUpdate task)
        {
            var taskForDb = HttpContext.Items["Task"] as MyTask;
            _mapper.Map(task, taskForDb);
            _db.Tasks.Update(taskForDb);
            await _db.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ServiceFilter(typeof(CheckTaskExistsAttribute))]
        public async Task<IActionResult> DeleteTask(int employeeId, int id)
        {
            var deletedTask = HttpContext.Items["Task"] as MyTask;
            _db.Tasks.Delete(deletedTask);
            await _db.SaveAsync();

            return NoContent();
        }
    }
}

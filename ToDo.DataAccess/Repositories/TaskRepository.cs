using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Extensions;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.DataAccess.Repositories
{
    public class TaskRepository: Repository<MyTask>, ITaskRepository
    {
        public TaskRepository(ToDoContext context):base(context){ }

        public async Task<IEnumerable<MyTask>> GetTasksAsync(int employeeId, TaskParameters parameters) =>
            await GetByConditions(t => t.EmployeeId == employeeId).SearchTasks(parameters).ToListAsync();

        public async Task<MyTask> GetTaskAsync(int employeeId, int id) =>
            await GetByConditions(t => t.EmployeeId == employeeId && t.Id == id).FirstOrDefaultAsync();
    }
}
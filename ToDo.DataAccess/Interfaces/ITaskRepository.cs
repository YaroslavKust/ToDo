using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.DataAccess.Interfaces
{
    public interface ITaskRepository: IRepository<MyTask>
    {
        Task<IEnumerable<MyTask>> GetTasksAsync(int employeeId, TaskParameters parameters);
        Task<MyTask> GetTaskAsync(int employeeId, int id);
    }
}
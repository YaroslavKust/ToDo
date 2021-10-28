using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.DataAccess.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(EmployeeParameters parameters);
        Task<Employee> GetEmployeeAsync(int id);
    }
}
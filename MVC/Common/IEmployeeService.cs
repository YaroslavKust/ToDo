using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entities.DTO;
using ToDo.Entities.RequestFeatures;

namespace MVC.Common
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees(EmployeeParameters parameters);
        Task<EmployeeDto> GetEmployee(int id);
        Task<bool> Create(EmployeeForManipulation employee);
        Task<bool> Update(int id, EmployeeForManipulation employee);
        Task<bool> Delete(int id);
    }
}
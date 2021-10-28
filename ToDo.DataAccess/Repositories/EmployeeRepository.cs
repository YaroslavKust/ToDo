using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess.Extensions;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.DataAccess.Repositories
{
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ToDoContext context):base(context){ }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(EmployeeParameters parameters) => 
            await GetAll().SearchEmployees(parameters).ToListAsync();

        public async Task<Employee> GetEmployeeAsync(int id) =>
            await GetByConditions(e => e.Id == id).FirstOrDefaultAsync();
    }
}
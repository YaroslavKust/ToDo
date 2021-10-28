using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.DTO;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace ToDo.Web.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IUnitOfWork _db;
        private IMapper _mapper;

        public EmployeeController(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeParameters parameters)
        {
            var result = await _db.Employees.GetEmployeesAsync(parameters);
            var employees = _mapper.Map<IEnumerable<EmployeeDto>>(result);

            return Ok(employees);
        }


        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var result = await _db.Employees.GetEmployeeAsync(id);
            var employee = _mapper.Map<EmployeeDto>(result);

            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForManipulation employee)
        {
            var employeeForDb = _mapper.Map<Employee>(employee);
            _db.Employees.Create(employeeForDb);
            await _db.SaveAsync();

            var result = _mapper.Map<EmployeeDto>(employeeForDb);

            return CreatedAtRoute("EmployeeById", new { id = employeeForDb.Id }, result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeForManipulation employee, int id)
        {
            var employeeForDb = await _db.Employees.GetEmployeeAsync(id);
            _mapper.Map(employee, employeeForDb);

            _db.Employees.Update(employeeForDb);
            await _db.SaveAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deletedEmployee = await _db.Employees.GetEmployeeAsync(id);
            _db.Employees.Delete(deletedEmployee);
            await _db.SaveAsync();

            return NoContent();
        }
    }
}

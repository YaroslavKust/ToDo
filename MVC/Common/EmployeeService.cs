using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDo.Entities.DTO;
using ToDo.Entities.Models;
using ToDo.Entities.RequestFeatures;

namespace MVC.Common
{
    public class EmployeeService: IEmployeeService
    {
        private IApiConnector _connector;
        private readonly string _employeeUrl;

        public EmployeeService(IApiConnector connector, IConfiguration config)
        {
            _connector = connector;
            _employeeUrl = config["ApiUrl"] + "/employees";
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees(EmployeeParameters parameters)
        {
            var response = await _connector.SendGetAsync(_employeeUrl);
            return await response.Content.ReadAsAsync<IEnumerable<EmployeeDto>>();
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            return null;

        }

        public async Task<bool> Create(EmployeeForManipulation employee)
        {
            return true;}

        public async Task<bool> Update(int id, EmployeeForManipulation employee)
        {
            return true;}

        public async Task<bool> Delete(int id)
        {
            return true;
        }
    }
}
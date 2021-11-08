using Microsoft.AspNetCore.Mvc;
using MVC.Common;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MVC.Models;
using ToDo.Entities.DTO;
using ToDo.Entities.RequestFeatures;

namespace ToDo.MVC.Controllers
{
    public class EmployeesController : Controller
    {
        private IApiConnector _connector;
        private readonly string _baseUrl;

        public EmployeesController(IApiConnector connector, IConfiguration config)
        {
            _connector = connector;
            _baseUrl = config["ApiUrl"] + "/employees";
        }

        public async Task<IActionResult> List(EmployeeParameters parameters)
        {
            var paramsDict = new Dictionary<string, string>
            {
                { "Name", parameters.Name },
                { "Speciality", parameters.Speciality },
                { "MinAge", parameters.MinAge.ToString() },
                { "MaxAge", parameters.MaxAge.ToString() },
                { "MinEmploymentDate", parameters.MinEmploymentDate.ToString("O") },
                { "MaxEmploymentDate", parameters.MaxEmploymentDate.ToString("O") }
            };

            var response = parameters is null ? 
                await _connector.SendGetAsync(_baseUrl) : 
                await _connector.SendGetAsync(_baseUrl, paramsDict);

            if (response.IsSuccessStatusCode)
            {
                return View(await response.Content.ReadAsAsync<IEnumerable<EmployeeDto>>());
            }

            return View();
        }


        public async Task<IActionResult> Details(int id)
        {

            var response = await _connector.SendGetAsync(_baseUrl + $"/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadAsAsync<EmployeeDto>();

                var respTasks = await _connector.SendGetAsync(_baseUrl + $"/{id}" + "/tasks");
                var tasks = await respTasks.Content.ReadAsAsync<IEnumerable<TaskDto>>();

                return View(new DetailsViewModel{Employee = employee, Tasks = tasks});
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _connector.SendGetAsync(_baseUrl + $"/{id}");

            if (response.IsSuccessStatusCode)
            {
                return View(await response.Content.ReadAsAsync<EmployeeDto>());
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeForManipulation employee)
        {
            var response = await 
                _connector.SendPutAsync<EmployeeForManipulation>(_baseUrl + $"/{id}",employee);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeForManipulation employee)
        {
            var response = await 
                _connector.SendPostAsync<EmployeeForManipulation>(_baseUrl,employee);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _connector.SendDeleteAsync(_baseUrl + $"/{id}");
            return RedirectToAction("List");
        }
    }
}

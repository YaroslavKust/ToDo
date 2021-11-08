using Microsoft.AspNetCore.Mvc;
using MVC.Common;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDo.Entities.DTO;

namespace ToDo.MVC.Controllers
{
    public class TaskController : Controller
    {
        private IApiConnector _connector;
        private readonly string _baseUrl;

        public TaskController(IApiConnector connector, IConfiguration config)
        {
            _connector = connector;
            _baseUrl = config["ApiUrl"];
        } 

        [HttpGet]
        public async Task<IActionResult> Edit(int employeeId, int id)
        {
            var request = BuildUrl(employeeId) + $"/{id}";
            var response = await _connector.SendGetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return View(await response.Content.ReadAsAsync<TaskDto>());
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int employeeId, int id, TaskForUpdate task)
        {
            var request = BuildUrl(employeeId) + $"/{id}";
            var response = await
                _connector.SendPutAsync<TaskForUpdate>(request, task);
            return RedirectToRoute("default", new { controller = "Employees", action = "Details", id = employeeId });
        }

        [HttpGet]
        public IActionResult Create(int employeeId)
        {
            ViewBag.EmployeeId = employeeId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int employeeId, TaskForCreate task)
        {
            var response = await
                _connector.SendPostAsync<TaskForCreate>(BuildUrl(employeeId), task);
            return RedirectToRoute("default", new { controller = "Employees", action = "Details", id = employeeId });
        }

        public IActionResult Delete(int employeeId, int id)
        {
            var request = BuildUrl(employeeId) + $"/{id}";
            var response = _connector.SendDeleteAsync(request);

            return RedirectToRoute("default", new { controller = "Employees", action = "Details", id = employeeId });
        }

        private string BuildUrl(int employeeId)
        {
            return _baseUrl + $"/employees/{employeeId}/tasks";
        }
    }
}

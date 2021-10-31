using Microsoft.AspNetCore.Mvc;

namespace ToDo.MVC.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult List()
        {
            return View();
        }


        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, object employee)
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(object employee)
        {
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return RedirectToAction("List");
        }
    }
}

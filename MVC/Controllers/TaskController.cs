using Microsoft.AspNetCore.Mvc;

namespace ToDo.MVC.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, object task)
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(object task)
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

using Microsoft.AspNetCore.Mvc;

namespace ToDo.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            return null;
        }


    }
}

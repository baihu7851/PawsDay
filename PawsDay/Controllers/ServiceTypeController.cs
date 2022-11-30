using Microsoft.AspNetCore.Mvc;

namespace PawsDay.Controllers
{
    public class ServiceTypeController : Controller
    {
        public IActionResult Homecare()
        {
            return View();
        }

        public IActionResult Homesalon()
        {
            return View();
        }

        public IActionResult Walking()
        {
            return View();
        }
    }
}
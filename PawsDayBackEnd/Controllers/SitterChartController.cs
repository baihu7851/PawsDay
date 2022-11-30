using Microsoft.AspNetCore.Mvc;

namespace PawsDayBackEnd.Controllers
{
    public class SitterChartController : Controller
    {
        public IActionResult SitterChart()
        {
            return View();
        }
    }
}
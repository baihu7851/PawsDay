using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.Interfaces;

namespace PawsDayBackEnd.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MemberCountAnalyze()
        {
            return View();
        }

    }
}

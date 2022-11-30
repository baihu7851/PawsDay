using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO.Account;
using PawsDayBackEnd.Helpers;

namespace PawsDayBackEnd.Controllers
{
    public class AuthController : Controller
    {

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

    }
}

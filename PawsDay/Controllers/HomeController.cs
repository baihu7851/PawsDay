using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PawsDay.Interfaces.Account;
using PawsDay.Interfaces.Index;
using PawsDay.ViewModels.Account;
using PawsDay.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIndexServices _services;
        private readonly IAccountManager _accountManager;

        public HomeController(ILogger<HomeController> logger, IIndexServices services, IAccountManager accountManager)
        {
            _logger = logger;
            _services = services;
            _accountManager = accountManager;
        }

        public IActionResult Index()
        {
            ViewData["MemberId"] = _accountManager.GetLoginMemberId();

            RecommendListViewModel recommendList = _services.ServicesRecommend();

            return View(recommendList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("NotFound");
        }
    }
}

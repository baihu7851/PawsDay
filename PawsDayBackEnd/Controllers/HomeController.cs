using Microsoft.AspNetCore.Http;
using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PawsDayBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Controllers
{
    
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Member()
        {
            return View();
        }
        public IActionResult Sitter()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult ContactUs()
        {                       
            return View();
        }
        public IActionResult ContactOrder()
        {            
            return View();
        }
        public IActionResult Order()
        {           
            return View();
        }
    }
}

using ApplicationCore.Entities;
//using Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Models.SitterCenter;
using PawsDay.Services.SitterCenter;
using PawsDay.Services.StaticWeb;
using PawsDay.ViewModels.StaticWeb;

namespace PawsDay.Controllers
{
    public class StaticWebController : Controller
    {
        private readonly ContactUsService _contactservices;
        public StaticWebController(ContactUsService services)
        {
            _contactservices = services;
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet] /*後端到前端*/
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost] /*前端到後端*/
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(ContactUsViewModel contanctVM)
        {
            if (!ModelState.IsValid)
            {
                return View(contanctVM);
            }
            else
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "表單已送出! 相關人員會盡快與您聯絡，謝謝~";

                _contactservices.CreateContact(contanctVM);
                
                return RedirectToAction("ContactUs");
            }
            
        }

        public IActionResult UserAgreement()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult InsutanceProtection()
        {
            return View();
        }

        public IActionResult ServiceProcess()
        {
            return View();
        }


        public IActionResult Support()
        {
            return View();
        }

        public IActionResult SitterAnswer()
        {
            return View();
        }
        public IActionResult MemberAnswer()
        {
            return View();
        }
        public IActionResult PaymentAnswer()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Newsone()
        {
            return View();
        }
        public IActionResult Newstwo()
        {
            return View();
        }
        public IActionResult Newsthree()
        {
            return View();
        }
    }
}
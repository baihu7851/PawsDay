using ApplicationCore.Common;
using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PawsDay.Services.BecomePetSitter;
using PawsDay.ViewModels.BecomePetsitter;
using System;
using System.Threading.Tasks;

namespace PawsDay.Controllers
{
    public class BecomePetsitterController : Controller
    {
        private readonly BecomePetSitterService _service;

        public BecomePetsitterController(BecomePetSitterService service)
        {
            _service = service;
        }

        public IActionResult PetsitterIntro()
        {
            return View();
        }

        [Authorize(Roles = AuthorizationConstants.NormalUser)]
        [HttpGet]
        public IActionResult PetsitterForm()
        {
            int memberId = _service.GetMemberId();

            var sitter = _service.GetSitter(memberId);
            if (sitter == null || sitter.RegisterStatus == (int)SitterStatus.Reject)
            {
                //未申請或申請未通過
                return View();
            }
            if (sitter.RegisterStatus == (int)SitterStatus.Approved)
            {
                //申請已通過
                return Redirect("../SitterCenter/Basic");
            }
            if (sitter.RegisterStatus == (int)SitterStatus.AwaitToCheck)
            {
                //申請待審核
                return Redirect("PetsitterDone");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = AuthorizationConstants.NormalUser)]
        [HttpPost]
        public async Task<IActionResult> PetsitterForm(PetsitterFormViewModel petsitterVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            petsitterVM.MemberId = _service.GetMemberId();
            await _service.isCreateOrUpdate(petsitterVM);
            return Redirect("PetsitterDone");
        }

        [Authorize(Roles = AuthorizationConstants.NormalUser)]
        public IActionResult PetsitterDone()
        {
            int memberId = _service.GetMemberId();
            DateTime registerTime = _service.GetSitter(memberId).CreateTime.AddHours(8);
            return View(registerTime);
        }
    }
}
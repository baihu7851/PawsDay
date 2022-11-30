using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PawsDay.Interfaces.Account;
using PawsDay.Models;
using PawsDay.Models.Account;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.MemberCenter;
using PawsDay.Services.SitterCenter;
using PawsDay.ViewModels.MemberCenter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PawsDay.WebApi.MemberCenter
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberCenterCityWebApiController : Controller
    {
        private readonly PersonInfoServices _personInfoServices;
        private readonly IAccountManager _accountManager;
        int userId;
        public MemberCenterCityWebApiController(PersonInfoServices personInfoServices, IAccountManager accountManager)
        {
            _personInfoServices = personInfoServices;
            _accountManager = accountManager;
            userId = _accountManager.GetLoginMemberId();
        }
        [HttpGet]
        public ActionResult<ResultDto> GetCityList()
        {
            //取得有被收藏的商品
            var response = _personInfoServices.GetCountyList(userId);

            return response;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PawsDay.Interfaces.Account;
using PawsDay.Models;
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
    [AllowAnonymous]
    public class MemberCenterCollectWebApiController : ControllerBase
    {
        private readonly CollectViewModelServices _collectViewModelServices;
        private readonly IAccountManager _accountManager;
        int userId;

        public MemberCenterCollectWebApiController(CollectViewModelServices collectViewModelServices, IAccountManager accountManager)
        {
            _collectViewModelServices = collectViewModelServices;
            _accountManager = accountManager;
            userId = _accountManager.GetLoginMemberId();
        }

        

        //處理另一包controllerDTO(含statuscode)

        //我的收藏
        [HttpGet]        
        public IActionResult GetCollectDate()
        {
            //取得有被收藏的商品
            var response = _collectViewModelServices.GetCollectList(userId);

            return Ok(response);
        }
        [HttpPost]
        public void DeleteCollect([FromBody] int collect)
        {
             _collectViewModelServices.RemoveCollect(collect);
        }


    }
}

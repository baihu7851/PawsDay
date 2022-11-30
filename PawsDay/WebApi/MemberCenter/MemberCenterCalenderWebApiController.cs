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
    public class MemberCenterCalenderWebApiController : ControllerBase
    {
        private readonly MemberCenterCalenderService _memberCenterCalenderService;
        private readonly IAccountManager _accountManager;
        int userId;

        public MemberCenterCalenderWebApiController(MemberCenterCalenderService memberCenterCalenderService, IAccountManager accountManager)
        {
            _memberCenterCalenderService = memberCenterCalenderService;
            _accountManager = accountManager;
            userId=_accountManager.GetLoginMemberId();
        }


        //處理另一包controllerDTO(含statuscode)

        //行事曆
        [HttpGet]
        public ActionResult<ResultDto> GetCalenderDate()
        {
            //取得有訂單的日期
            var response = _memberCenterCalenderService.GetCalenderList(userId);

            return response;
        }      

    }
}

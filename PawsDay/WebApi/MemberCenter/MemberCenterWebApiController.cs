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
    public class MemberCenterWebApiController : ControllerBase
    {
        
        private readonly MemberCenterSidebarServices _memberCenterSidebarServices;
        private readonly ChatroomViewModelService _chatroomViewModelService;
        private readonly IAccountManager _accountManager;
        int userId;
        public MemberCenterWebApiController(MemberCenterSidebarServices memberCenterSidebarServices, ChatroomViewModelService chatroomViewModelService, IAccountManager accountManager)
        {
            
            _memberCenterSidebarServices = memberCenterSidebarServices;
            _chatroomViewModelService = chatroomViewModelService;
            _accountManager = accountManager;
            userId = _accountManager.GetLoginMemberId();
        }

        //處理另一包controllerDTO(含statuscode)

       

        //上傳大頭貼
        [HttpPost]
        public async Task<ActionResult<ResultDto>> UpdateUserImage([FromBody] UserImageDto data)
        {
            var response = await _memberCenterSidebarServices.UpdateUserImage(data);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto>> ChatroomSisterDetail([FromBody] ChatroomSisterDetailDTO input)
        {           
            var result= await _chatroomViewModelService.CreateSisterDetail(input.Message, userId, input.SitterId);      
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto>> OrderContact([FromBody] OrderContactDTO input)
        {

            var result = await _chatroomViewModelService.CreateOrderContact(input);            
            return result;
        }

    }
}

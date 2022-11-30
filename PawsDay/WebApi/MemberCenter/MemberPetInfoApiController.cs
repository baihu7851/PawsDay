using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Interfaces.Account;
using PawsDay.Models.MemberCenter;
using PawsDay.Services.MemberCenter;
using PawsDay.Services.SendGridServices;
using PawsDay.ViewModels.MemberCenter;

namespace PawsDay.WebApi.MemberCenter
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberPetInfoApiController : ControllerBase
    {
        private readonly PawsDayContext _context;
        private readonly MemberPetInfoService _petInfoService;
        private readonly IAccountManager _accountManager;

        private int userId;

        public MemberPetInfoApiController(PawsDayContext context, MemberPetInfoService petInfoService, IAccountManager accountManager)
        {
            _context = context;
            _petInfoService = petInfoService;
            _accountManager = accountManager;
            userId = _accountManager.GetLoginMemberId();
        }

        [HttpPost] //更新
        public PetInfoDTO UpdatePetInformation([FromBody] PetInfoDTO memberPetInfoDto)
        {
            _petInfoService.UpdatePetInfo(memberPetInfoDto);

            return memberPetInfoDto;
        }

        [HttpDelete] //刪除
        public IActionResult Delete(int petid)
        {
            _petInfoService.DeleteDisposition(petid);
            _petInfoService.DeletePetInfo(petid);

            return Ok();
        }
    }
}

using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.Services;

namespace PawsDayBackEnd.WebApi
{
    
    [Authorize(Roles = AuthorizationConstants.Administrator)]
    public class MemberApiController : BaseApiController
    {
        private readonly MemberServices _memberServices;
        public MemberApiController(MemberServices memberServices)
        {
            _memberServices = memberServices;
        }



        //Status查詢(1=正常、3=停權)

        [HttpGet]
        public ActionResult<ApiResultDto> MemberListByStatus(int status,int index,int count)
        {
            var response = _memberServices.GetMemberListByStatus(status,index,count);
            return response;

        }

        //Name查詢
        [HttpGet]
        public ActionResult<ApiResultDto> MemberListByName(string name)
        {
            var response = _memberServices.GetMemberListByName(name);
            return response;

        }

        //ID查詢
        [HttpGet]
        public ActionResult<ApiResultDto> MemberListByID(int id)
        {
            var response = _memberServices.GetMemberListByID(id);
            return response;

        }

        //Email查詢
        [HttpGet]
        public ActionResult<ApiResultDto> MemberListByMail(string email)
        {
            var response = _memberServices.GetMemberListByMail(email);
            return response;

        }

        //更新狀態
        [HttpPost]
        public ActionResult<ApiResultDto> UpdateMemberStatus(int id, int updatestatus)
        {
            var response = _memberServices.UpdateUniStatus(id, updatestatus);
            return response;
        }

        //取得保姆資訊
        [HttpGet]
        public ActionResult<ApiResultDto> GetMemberInfo(int id)
        {
            var response = _memberServices.GetInfo(id);
            return response;
        }
    }
}

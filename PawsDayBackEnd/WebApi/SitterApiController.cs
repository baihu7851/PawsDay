using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.Services;
using System.Collections.Generic;

namespace PawsDayBackEnd.WebApi
{

    [Authorize(Roles = AuthorizationConstants.Administrator)]
    public class SitterApiController : BaseApiController
    {
        private readonly SitterServices _sitterServices;
        public SitterApiController(SitterServices sitterServices)
        {
            _sitterServices = sitterServices;
        }


        //Status查詢(0=未審核、1=通過、2=未通過)
        [HttpGet] 
        public ActionResult<ApiResultDto> SitterListByStatus(int status,int index,int count)
        {
            var response = _sitterServices.GetSitterListByStatus(status,index,count);
            return response;
            
        }

        //Name查詢
        [HttpGet]
        public ActionResult<ApiResultDto> SitterListByName(string name)
        {
            var response = _sitterServices.GetSitterListByName(name);
            return response;

        }

        //ID查詢
        [HttpGet]
        public ActionResult<ApiResultDto> SitterListByID(int id)
        {
            var response = _sitterServices.GetSitterListByID(id);
            return response;

        }

        //Email查詢
        [HttpGet]
        public ActionResult<ApiResultDto> SitterListByMail(string email)
        {
            var response = _sitterServices.GetSitterListByMail(email);
            return response;

        }

        //個別更新狀態
        [HttpPost]
        public ActionResult<ApiResultDto> UpdateSitterStatus(int id,int updatestatus)
        {
            var response = _sitterServices.UpdateUniStatus(id, updatestatus);
            return response;
        }

        //批次更新狀態
        [HttpPost]
        public ActionResult<ApiResultDto> ApproveSitterGroupStatus(List<int> id)
        {
            var response = _sitterServices.UpdateRangeStatus(id);
            return response;
        }

        //取得保姆資訊
        [HttpGet]
        public ActionResult<ApiResultDto> GetSitterInfo(int id)
        {
            var response = _sitterServices.GetInfo(id);
            return response;
        }
    }
}

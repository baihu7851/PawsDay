using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PawsDay.Models;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.MemberCenter;
using PawsDay.Services.SitterCenter;
using PawsDay.ViewModels.MemberCenter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PawsDay.WebApi.SitterCenter
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SitterCenterWebApiController : ControllerBase
    {
        private readonly SitterCenterServices _SitterCenterServices;
        private readonly SitterCenterBasicServices _SitterCenterBasicServices;
        private readonly SitterCenterOrderServices _SitterCenterOrderServices;
        
        public SitterCenterWebApiController(SitterCenterServices twitterCenterServices, SitterCenterOrderServices sitterCenterOrderServices, SitterCenterBasicServices sitterCenterBasicServices)
        {
            _SitterCenterServices = twitterCenterServices;
            _SitterCenterOrderServices = sitterCenterOrderServices;
            _SitterCenterBasicServices = sitterCenterBasicServices;
        }


        [HttpPost]
        public async Task<ActionResult<ResultDto>> UpdateUserImage([FromBody] UserImageDto data)
        {
            var response = await _SitterCenterBasicServices.UpdateUserImage(data);
            return response;
        }


        #region 商品規格

        [HttpGet]
        public ActionResult<ResultDto> GetServicetDetailById(int productid)
        {
            var response = _SitterCenterServices.GetProducttDetailById(productid);

            return response;
        }


        [HttpPost]
        public async Task<ActionResult<ResultDto>> UpdateServicetDetailById(ServiceSettingDto request)
        {
            var response = await  _SitterCenterServices.CreateOrUpdate(request);
            return response;
        }


        [HttpPost]
        public ActionResult<ResultDto> UpdateProductBasic([FromBody] ProductBasicDto data)
        {
            var response =  _SitterCenterServices.CreateOrUpdateBasic(data);
            return response;
        }

        //預覽商品促銷資訊、服務說明
        [HttpGet]
        public ActionResult<ResultDto> GetPreviewData(int productid)
        {
            var response = _SitterCenterServices.GetDiscountandIntro(productid);
            return response;
        }

        #endregion

        #region 促銷
        //GetSales、 CreateSales+UpdateSales、 DeleteSales
        [HttpGet]
        public ActionResult<ResultDto> GetProductBox() 
        {
            //促銷Box
            var response = _SitterCenterServices.GetProductList();
            return response;
        }

        [HttpGet]
        public ActionResult<ResultDto> GetSales(int id)
        {
            //從service傳整包資料，處理後給前端
            var response = _SitterCenterServices.GetSales(id);

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto>> UpdateSales([FromBody]DiscountDto data)
        {
            //從前端拿資料，傳給service
            var response = await _SitterCenterServices.CreateOrUpdateSales(data);
            
            return response;
        }

        [HttpDelete]
        public async Task<ActionResult<ResultDto>> DeleteSales(int id)
        {
            var response = await _SitterCenterServices.DeteleSales(id);

            return response;
        }
        #endregion

        #region 行事曆
        [HttpGet]
        public ActionResult<ResultDto> GetCalenderDate() 
        {
            //取得有訂單的日期
            var response = _SitterCenterOrderServices.GetCalenderList();
            
            return response; 
        }
        #endregion

        #region 報表
        [HttpGet]
        public ActionResult<ResultDto> GetChart(string type)
        {
            var response = _SitterCenterServices.GetChartData(type);
            return response;
        }
        #endregion

        #region 聊天室
        [HttpPost]
        public async Task<ActionResult<ResultDto>> ChatroomMemberDetail([FromBody] ChatroomMemberDetailDto input)
        {
            var result = await _SitterCenterOrderServices.CreateCustomerContactDetail(input.Message, input.MemberId);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto>> OrderContact([FromBody] ChatroomOrderDetailDto input)
        {

            var result = await _SitterCenterOrderServices.CreateOfficialContactDetail(input.Message,input.OrderID);
            return result;
        }
        #endregion
    }
}

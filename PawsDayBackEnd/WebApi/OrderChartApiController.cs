using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.OrderChart;
using PawsDayBackEnd.Services;
using System.Collections.Generic;
using System.Text.Json;

namespace PawsDayBackEnd.WebApi
{
    //[Authorize(Roles = AuthorizationConstants.Administrator)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    //public class OrderChartApiController : BaseApiController
    public class OrderChartApiController : Controller
    {
        private readonly ChartOrderServices _chartOrderServices;

        public OrderChartApiController(ChartOrderServices chartOrderServices)
        {
            _chartOrderServices = chartOrderServices;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetMonthlyData()
        {
            var response = new ApiResultDto();
            var dto = _chartOrderServices.GetTotalSales();
            
            if(dto is null)
            {
                response.Status = DTO.StatusCode.Failed;
                response.Data = string.Empty;
                response.Message = "查無訂單資訊";
                return response;
            }
            response.Status = DTO.StatusCode.Success;
            response.Data = dto;
            response.Message = "完成!";


            return response;
        }


        

        [HttpGet]
        public ActionResult<ApiResultDto> GetTotalRevenue()
        {
            var response = new ApiResultDto();
            var dto = _chartOrderServices.GetGrossTotalRevenu();
            if (dto == 0)
            {
                response.Status = DTO.StatusCode.Failed;
                response.Data = string.Empty;
                response.Message = "查詢失敗";
                return response;
            }
            response.Status = DTO.StatusCode.Success;
            response.Data = dto;
            response.Message = "查詢完成!";


            return response;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetTotalSalesQuan()
        {
            var response = new ApiResultDto();
            var dto = _chartOrderServices.GetTotalSalesQuan();
            if (dto == 0)
            {
                response.Status = DTO.StatusCode.Failed;
                response.Data = string.Empty;
                response.Message = "查無資料";
                return response;
            }
            response.Status = DTO.StatusCode.Success;
            response.Data = dto;
            response.Message = "查詢完成!";


            return response;
        }

        //[HttpGet]
        //public ActionResult<ApiResultDto> GetTotalSalesQuan()
        //{
        //    var response = new ApiResultDto();
        //    var dto = _chartOrderServices.GetTotalSalesQuan();
        //    if (dto == 0)
        //    {
        //        response.Status = DTO.StatusCode.Failed;
        //        response.Data = string.Empty;
        //        response.Message = "查無資料";
        //        return response;
        //    }
        //    response.Status = DTO.StatusCode.Success;
        //    response.Data = dto;
        //    response.Message = "查詢完成!";


        //    return response;
        //}

        [HttpGet]
        public ActionResult<ApiResultDto> CreateSercicePie()
        {
            var response = new ApiResultDto();
            var dto = _chartOrderServices.GetServicePie();

            if (dto is null)
            {
                response.Status = DTO.StatusCode.Failed;
                response.Data = string.Empty;
                response.Message = "查無資料";
                return response;
            }
            response.Status = DTO.StatusCode.Success;
            response.Data = dto;
            response.Message = "查詢完成!";


            return response;
        }

    }
}

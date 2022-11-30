using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.ChartProduct;
using PawsDayBackEnd.Services;

namespace PawsDayBackEnd.WebApi
{
    [Authorize(Roles = AuthorizationConstants.Administrator)]
    public class ChartProductApiController : BaseApiController
    {
        private readonly ChartProductServices _services;

        public ChartProductApiController(ChartProductServices services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetProductQuantity()
        {
            var response = _services.GetProductQuantity();
            return response;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetSaleQuantity()
        {
            var response = _services.GetSaleQuantity();
            return response;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetCreateQuantity()
        {
            var response = _services.GetCreateQuantity();
            return response;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetServiceTypeQuantity()
        {
            var response = _services.GetServiceTypeQuantity();
            return response;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetEvaluationTop()
        {
            var response = _services.GetEvaluationTop();
            return response;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetOrderTop()
        {
            var response = _services.GetOrderTop();
            return response;
        }

    }
}

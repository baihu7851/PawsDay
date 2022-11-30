using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.Services;

namespace PawsDayBackEnd.WebApi
{
    [Authorize(Roles = AuthorizationConstants.Administrator)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SitterChartApiController : BaseApiController
    {
        private readonly SitterChartService _service;

        public SitterChartApiController(SitterChartService sitterChartService)
        {
            _service = sitterChartService;
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetApprovedSitter()
        {
            return _service.GetApprovedSitter();
        }

        [HttpGet]
        public ActionResult<ApiResultDto> CountCreateQuantity()
        {
            return _service.CountCreateQuantity();
        }

        [HttpGet]
        public ActionResult<ApiResultDto> CountStatusQuantity()
        {
            return _service.CountStatusQuantity();
        }

        [HttpGet]
        public ActionResult<ApiResultDto> GetSitterTop()
        {
            return _service.GetSitterTop();
        }
    }
}
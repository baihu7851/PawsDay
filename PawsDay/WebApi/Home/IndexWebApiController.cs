using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.Home;
using PawsDay.Services.Layout;

namespace PawsDay.WebApi.Home
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IndexWebApiController : ControllerBase
    {
        private readonly IndexServices  _services;

        public IndexWebApiController(IndexServices services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<bool> GetCollect(int memberId,int productId)
        {
            var result = _services.GetCollect(memberId, productId);
            if (result.IsSuccess == false)
            {
                return false;
            }
            result.IsSuccess = true;
            var collect = (bool)result.Data;
            return collect;
        }
    }
}

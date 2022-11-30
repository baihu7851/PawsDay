using Microsoft.AspNetCore.Mvc;
using PawsDay.Interfaces.Account;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.Product;
using PawsDay.ViewModels.Product;
using System.Collections.Generic;

namespace PawsDay.WebApi.Product
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductWebApiController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly ProductServices _services;
        public ProductWebApiController(ProductServices services, IAccountManager accountManager)
        {
            _services = services;
            _accountManager = accountManager;
        }

        [HttpPost]
        public ActionResult<ResultDto> CreateCart([FromBody] ProductToCartDto selected)
        {
            var response = _services.CreateCartWebApi(selected);
            return response;
        }

        [HttpGet]
        public ActionResult<List<int>> GetDisabledTime(int productId,int year,int month,int day)
        {
            var result = _services.GetDisabledTimeWebApi(productId,year, month, day);
            var times=new List<int>();
            if (result.IsSuccess == false)
            {
                times =null;
                return times;
            }
            result.IsSuccess = true;
            times = (List<int>)result.Data;
            return times;
        }
        [HttpGet]
        public ActionResult<decimal> GetPetPrice(int productId, string types, string times)
        {
            var result = _services.GetPriceWebApi(productId, types, times);
            if (result.IsSuccess == false)
            {
                return 0;
            }
            result.IsSuccess = true;
            var price = (decimal)result.Data;

            return price;
        }

        [HttpPost]
        public ActionResult<ResultDto> CreateCollect([FromBody] CollectDto collect)
        {
            var response = _services.CreateCollectWebApi(collect);
            return response;
        }

        [HttpDelete]
        public ActionResult<ResultDto> DeleteCollect([FromBody] CollectDto collect)
        {
            var response = _services.DeleteCollectWebApi(collect);
            return response;
        }

    }
}

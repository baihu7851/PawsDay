using ApplicationCore.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.Layout;
using PawsDay.Services.ShoppingCart;
using PawsDay.ViewModels.Layout;
using System.Collections.Generic;

namespace PawsDay.WebApi.Layout
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LayoutWebApiController : ControllerBase
    {
        private readonly MenuServices _services;
        public LayoutWebApiController(MenuServices services)
        {
            _services = services;
        }
        [HttpGet]
        public ActionResult<List<CartMenuViewModel>> GetShoppingCart(int memberId)
        {
            var result = _services.ShoppingCartWebApi(memberId);
            List<CartMenuViewModel> cartMenu = (List<CartMenuViewModel>)result.Data;
            if (result.IsSuccess == false)
            {
                return null;
            }
            else
            {
                result.IsSuccess = true;
                return cartMenu;
            }
        }
        [HttpPost]
        public ActionResult<List<CartMenuViewModel>> NotLoginGetCart([FromBody]List<NotLoginCartDto> carts)
        {
            var result = _services.NotLoginCartWebApi(carts);
            List<CartMenuViewModel> cartMenu = (List<CartMenuViewModel>)result.Data;
            if (result.IsSuccess == false)
            {
                return null;
            }
            else
            {
                result.IsSuccess = true;
                return cartMenu;
            }
        }
    }
}

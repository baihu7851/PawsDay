using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PawsDay.Models.ShoppingCart.WebApi.BaseModel;
using PawsDay.Models.ShoppingCart.WebApi.Enums;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.ShoppingCart;
using PawsDay.Services.SitterCenter;
using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.WebApi.ShoppingCart
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class ShoppingCartWebApiController : ControllerBase
    {
        private readonly CartServices _cartServices;
        private readonly BookingServices _bookingServices;
        private readonly SitterCenterServices _SitterCenterServices;
        public ShoppingCartWebApiController(CartServices cartServices, BookingServices bookingServices, SitterCenterServices sitterCenterServices)
        {
            _cartServices = cartServices;
            _bookingServices = bookingServices;
            _SitterCenterServices = sitterCenterServices;
        }

        [HttpGet]
        public ActionResult<decimal> GetPetPrice(int cartId, int productId, int petType, int shapeType)
        {
            var result = _cartServices.GetProductUnitPrice( cartId, productId,petType,shapeType);
            if (result.IsSuccess == false)
            {
                
                return 0;
            }
            result.IsSuccess = true;
            var price = (decimal)result.Body;

            return price;
        }

        [HttpGet]
        public ActionResult<decimal> GetVisitorPetPrice(string timeString, int productId, int petType, int shapeType)
        {
            var result = _cartServices.VisitorGetProductUnitPrice(timeString, productId, petType, shapeType);
            if (result.IsSuccess == false)
            {

                return 0;
            }
            result.IsSuccess = true;
            var price = (decimal)result.Body;

            return price;
        }

        [HttpPut]
        public ActionResult<BaseResult> Update([FromBody] List<CartDetailDTO> sourceList)
        {
            var result = _cartServices.UpdateCartItem(sourceList);
            if (result.IsSuccess == false)
            {
                result.IsSuccess = false;
                return result;
            }
            result.IsSuccess = true;

            return result;
        }

        [HttpDelete]
        public ActionResult<BaseResult> Delete(DeleteDTO dto)
        {
            var result = _cartServices.DeleteSelectedItem(dto.ToDelete);

            if (result.IsSuccess == false)
            {
                result.IsSuccess = false;
                return result;
            }
            result.IsSuccess = true;

            return result;

        }

        public class DeleteDTO
        {
            public string ToDelete { get; set; }
        }


    }
}

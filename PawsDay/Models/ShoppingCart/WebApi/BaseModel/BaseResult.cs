using PawsDay.Models.ShoppingCart.WebApi.Enums;
using System;

namespace PawsDay.Models.ShoppingCart.WebApi.BaseModel
{
    public class BaseResult
    {
        public BaseResult()
        {
            IsSuccess = true;
            Response = ApiStatus.Success;
        }

        public bool IsSuccess { get; set; }
        public object Body { get; set; }
        public ApiStatus Response { get; set; }
        public string Message { get; set; }
    }
}

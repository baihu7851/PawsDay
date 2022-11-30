using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.Services;

namespace PawsDayBackEnd.WebApi
{
    //[Authorize(Roles = AuthorizationConstants.Administrator)]
    [AllowAnonymous]
    public class ProductApiController : BaseApiController
    {
        private readonly ProductServices _productservices;
        public ProductApiController(ProductServices productServices) 
        {
            _productservices = productServices;
        }

        #region 查詢
        //以狀態查詢
        [HttpGet]
        public ActionResult<ApiResultDto> ProductListByStatus(int status, int index,int count)
        {
            var response = _productservices.GetProductListByStatus(status, index, count);
            return response;
        }

        //以服務類型查詢
        [HttpGet]
        public ActionResult<ApiResultDto> ProductListByType(int type,int status, int index, int count)
        {
            var response = _productservices.GetProductListByServiceType(type, status, index, count);
            return response;
        }

        //以ID查詢
        [HttpGet]
        public ActionResult<ApiResultDto> ProductListById(int id)
        {
            var response = _productservices.GetProductListByProductId(id);
            return response;
        }

        //以保姆ID查詢
        [HttpGet]
        public ActionResult<ApiResultDto> ProductListBySitterId(int id)
        {
            var response = _productservices.GetProductListBySitterId(id);
            return response;
        }

        //以保姆名字查詢
        [HttpGet]
        public ActionResult<ApiResultDto> ProductListBySitterName(string name)
        {
            var response = _productservices.GetProductListBySitterName(name);
            return response;
        }

        #endregion

        #region 商品資訊
        [HttpGet]
        public ActionResult<ApiResultDto> ProductInfo(int id)
        {
            var response = _productservices.GetInfo(id);
            return response;
        }
        #endregion

        #region 更改狀態

        [HttpPost]
        public ActionResult<ApiResultDto> ProductStatus(int id,int status)
        {
            var response = _productservices.UpdateProductStatus(id, status);
            return response;
        }

        [HttpPost]
        public ActionResult<ApiResultDto> Delete(int id)
        {
            var response = _productservices.ProductDelete(id);
            return response;
        }

        #endregion
    }
}

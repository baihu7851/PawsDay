using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Member;
using PawsDayBackEnd.DTO.Product;
using SendGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Xml.Linq;

namespace PawsDayBackEnd.Services
{
    public class ProductServices
    {
        #region 建構式
        private readonly IRepository<Product> _product;
        private readonly IRepository<ProductImage> _productimage;
        private readonly IRepository<ProductServicePetType> _productservicetype;
        private readonly IRepository<ProductDiscount> _productdiscount;
        private readonly IRepository<RegisterSitter> _registersitter;
        private readonly IRepository<ServiceType> _servicetype;
        public ProductServices(IRepository<Product> product,IRepository<ProductImage> productimage, IRepository<ProductServicePetType> productservicetype, IRepository<ProductDiscount> productdiscount, IRepository<RegisterSitter> registersitter,IRepository<ServiceType> servicetype) 
        {
            _product = product;
            _productimage = productimage;
            _productservicetype = productservicetype;
            _registersitter = registersitter;
            _productdiscount = productdiscount;
            _servicetype=servicetype; 
        }
        #endregion

        #region 查詢
        //以商品狀態查詢
        public ApiResultDto GetProductListByStatus(int status, int index, int takecount)
        {
            var raw = _product.GetAllReadOnly().Where(p => p.ProductStatus == status && p.IsDelete == false).Skip(index).Take(takecount).ToList();
            var count = _product.GetAllReadOnly().Count(p => p.ProductStatus == status && p.IsDelete == false);
            return GetProductList(raw, count);
        }
        //以服務類型查詢
        public ApiResultDto GetProductListByServiceType(int type,int status, int index, int takecount)
        {
            var raw = _product.GetAllReadOnly().Where(p => p.ServiceType==type && p.ProductStatus==status && p.IsDelete == false).Skip(index).Take(takecount).ToList();
            var count = _product.GetAllReadOnly().Count(p => p.ServiceType == type && p.ProductStatus == status && p.IsDelete == false);
            return GetProductList(raw, count);
        }
        //以商品ID查詢
        public ApiResultDto GetProductListByProductId(int id)
        {
            var raw = _product.GetAllReadOnly().Where(p => p.ProductId==id && p.IsDelete == false).ToList();
            var count = raw.Count();
            return GetProductList(raw, count);
        }
        //以保姆ID查詢
        public ApiResultDto GetProductListBySitterId(int id)
        {
            var raw = _product.GetAllReadOnly().Where(p => p.SitterId == id && p.IsDelete == false).ToList();
            var count = raw.Count();
            return GetProductList(raw, count);
        }
        //以保姆名字查詢
        public ApiResultDto GetProductListBySitterName(string name)
        {
            var raw = _product.GetAllReadOnly().Where(p => _registersitter.GetAllReadOnly().Where(s=>s.SitterName.ToUpper().Contains(name.ToUpper())).Select(s=>s.SitterId).Contains(p.SitterId) && p.IsDelete == false).ToList();
            var count = raw.Count();
            return GetProductList(raw, count);
        }


        //共用method:組成Dto包出去
        public ApiResultDto GetProductList(List<Product> raw, int count)
        {
            if (count == 0)
            {
                var response = new ApiResultDto();
                response.Message = "Not Found";
                return response;
            }

            var data = from p in raw
                    join t in _servicetype.GetAllReadOnly() on p.ServiceType equals t.ServiceTypeId
                    select new ProductListDto 
                    {
                        ProductId = p.ProductId,
                        SitterId = p.SitterId,
                        Service = t.TypeName,
                        CreateTime = p.CreateTime.AddHours(8),
                        Status = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ProductStatus)p.ProductStatus),
                        Count = count
                    };


            return new ApiResultDto(data);
        }



        #endregion

        #region 商品資訊
        public ApiResultDto GetInfo(int id)
        {
            var product = _product.GetById(id);
            var sitter = _registersitter.GetAllReadOnly().First(s => s.MemberId == product.SitterId);
            var price = _productservicetype.GetAllReadOnly().Where(t => t.ProductId == id).Select(t=> new PriceInfoDto 
            { 
                Pet= EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)t.PetType),
                Shape= EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)t.ShapeType),
                Price = t.Price,
                NightPrice=t.OvernightPrice
            });
            var img =_productimage.GetAllReadOnly().Where(i => i.ProductId == id).Select(i=>i.Image);
            int quantity;
            decimal discount;
            var hasdiscount = _productdiscount.GetAllReadOnly().FirstOrDefault(d => d.ProductId == id);
            if (hasdiscount == null)
            {
                quantity = 0;
                discount = 0m;
            }
            else
            {
                quantity = hasdiscount.Quantity;
                discount = hasdiscount.Discount;

            }

            var info = new ProductInfoDto 
            {
                ProductId=id,
                Status = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ProductStatus)product.ProductStatus),
                SitterName =sitter.SitterName,
                SitterInfo=sitter.SitterInfo,
                ServiceType= _servicetype.GetAllReadOnly().First(t=>t.ServiceTypeId==product.ServiceType).TypeName,
                ProductInfo=product.Introduce,
                ImageUrl=img.ToList(),
                DiscountQuantity= quantity,
                Discount= discount,
                Price= price.ToList()
            };
            return new ApiResultDto(info);

        }
        #endregion

        #region 更改狀態

        public ApiResultDto UpdateProductStatus(int id, int status)
        {
            var response = new ApiResultDto();
            var product = _product.GetById(id);
            if (product == null)
            {
                response.Status = StatusCode.Failed;
                response.Message = "輸入ID錯誤";
                return response;
            }

            product.ProductStatus = status;
            response = UpdateProduct(product);
            return response;
        }

        public ApiResultDto ProductDelete(int id)
        {
            var product = _product.GetById(id);
            product.IsDelete = true;
            var response = UpdateProduct(product);
            return response;
        }

        private ApiResultDto UpdateProduct(Product product)
        {
            var response = new ApiResultDto();
            try
            {
                _product.Update(product);
                response.Status = StatusCode.Success;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗{ex}";
            }
            return response;
        }

        #endregion

    }
}

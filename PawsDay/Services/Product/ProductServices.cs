using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet.Actions;
using Dapper;
using Infrastructure.Model;
using isRock.LineBot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PawsDay.Interfaces.Account;
using PawsDay.Models.ShoppingCart.WebApi.Enums;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.ViewModels.Home;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.Product;
using PawsDay.ViewModels.SearchProduct;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using Order = ApplicationCore.Entities.Order;

namespace PawsDay.Services.Product
{
    public class ProductServices
    {
        private readonly IAccountManager _accountManager;
        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _sitter;
        private readonly IRepository<ApplicationCore.Entities.Product> _product;
        private readonly IRepository<ProductDiscount> _discount;
        private readonly IRepository<ProductImage> _image;
        private readonly IRepository<ProductServiceTime> _time;
        private readonly IRepository<ServiceType> _servicetype;
        private readonly IRepository<Order> _order;
        private readonly IRepository<OrderSchedule> _orderschedule;
        private readonly IRepository<Evaluation> _evaluation;
        private readonly IRepository<ProductServiceArea> _area;
        private readonly IRepository<County> _county;
        private readonly IRepository<District> _dist;
        private readonly IRepository<ProductServicePetType> _pettype;
        private readonly IRepository<Schedule> _schedule;
        private readonly IRepository<Collect> _collect; 
        private readonly IRepository<Cart> _cart;
        private readonly IRepository<CartDetail> _cartdetail;
        private readonly IRepository<CartSchedule> _cartschedule;
        private readonly IConfiguration _configuration;
        public ProductServices(IRepository<Member> member, IRepository<RegisterSitter> sitter, IRepository<ApplicationCore.Entities.Product> product, IRepository<ProductDiscount> discount, IRepository<ProductImage> image, IRepository<ProductServiceArea> area, IRepository<ProductServicePetType> pettype, IRepository<ProductServiceTime> time, IRepository<ServiceType> servicetype, IRepository<Order> order, IRepository<Evaluation> evaluation, IRepository<County> county, IRepository<District> dist, IRepository<Schedule> schedule, IRepository<Cart> cart, IRepository<CartDetail> cartdetail, IRepository<CartSchedule> cartschedule, IRepository<Collect> collect, IAccountManager accountManager, IRepository<OrderSchedule> orderschedule, IConfiguration configuration)
        {
            _member = member;
            _sitter = sitter;
            _product = product;
            _discount = discount;
            _image = image;
            _time = time;
            _servicetype = servicetype;
            _order = order;
            _evaluation = evaluation;
            _area = area;
            _county = county;
            _dist = dist;
            _pettype = pettype;
            _schedule = schedule;
            _collect = collect;

            _cart = cart;
            _cartdetail = cartdetail;
            _cartschedule = cartschedule;
            _accountManager = accountManager;
            _orderschedule = orderschedule;
            _configuration = configuration;
        }

        public int GetMemberId()
        {
            var memberId = _accountManager.GetLoginMemberId();
            return memberId;
        }
        #region 商品卡
        public List<ProductCardDto> GetProductCardList()
        {
            var customerId = _accountManager.GetLoginMemberId();
            var productList = _product.GetAllReadOnly().Where(p=>p.ProductStatus==(int)ProductStatus .OnSale&& p.IsDelete==false).ToList();
            var cardList = new List<ProductCardDto>();
            var servicetypes = _servicetype.GetAllReadOnly().ToList();
            var images = _image.GetAllReadOnly().Where(x => productList.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            var orders = _order.GetAllReadOnly().Where(x => productList.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            var collects = _collect.GetAllReadOnly().Where(x => productList.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            var pettypes = _pettype.GetAllReadOnly().Where(x => productList.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            var areas = _area.GetAllReadOnly().Where(x => productList.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            var countys = _county.GetAllReadOnly().ToList();
            var districts = _dist.GetAllReadOnly().ToList();
            var sitters = _sitter.GetAllReadOnly().Where(x => productList.Select(y => y.SitterId).Contains(x.MemberId)).ToList();
            var times = _time.GetAllReadOnly().Where(x => productList.Select(y => y.ProductId).Contains(x.ProductId)).ToList();
            foreach (var product in productList)
            {
                var evaluation = GetEvaluationDapper(product.ProductId);
                var sitter = sitters.First(x => x.MemberId == product.SitterId);
                var imgfirst = images.First(x => x.ProductId == product.ProductId&&x.Sort == 1).Image;
                var area = areas.Where(a => a.ProductId == product.ProductId).ToList();
                var list = new ProductCardDto
                {
                    ProductId = product.ProductId,
                    SitterName = sitter.SitterName,
                    ServiceType = servicetypes.First(x => x.ServiceTypeId == product.ServiceType).TypeName,
                    Image = imgfirst,
                    EvaluationAverage = (double)evaluation[0].EvaluationAvg,
                    EvaluationQuantity = evaluation[0].EvaluationCount,
                    OrderQuantity = orders.Where(x => x.ProductId == product.ProductId).Count(),
                    Collect = collects.Any(c => c.ProductId == product.ProductId && c.MemberId == customerId),
                    Price = pettypes.First(t => t.ProductId == product.ProductId).Price,
                    Introduce = product.Introduce,
                    County = string.Join(",", area.Select(a => countys.First(c => c.CountyId == a.County).CountyName).ToList()),
                    District = string.Join(",", area.Select(a => districts.First(c => c.DistrictId == a.District).DistrictName).ToList()),
                    ServiceArea = GetServiceArea(area, countys,districts),
                    FilterPetTypes = string.Join(",", pettypes.Where(t => t.ProductId == product.ProductId).ToList().Select(t => (PetType)t.PetType)),
                    FilterDays = string.Join(",", times.Where(t => t.ProductId == product.ProductId).Select(t => (ServieDay)t.ServiceDay)),
                    FilterTimes = string.Join(",", times.Where(t => t.ProductId == product.ProductId).Select(t => (ServiceTime)t.ServicePartTime))
                };
                cardList.Add(list);
            }

            return cardList;
        }
        #endregion

        #region 搜尋頁篩選
        public SearchFilterDto FilterArea()
        {
            var countys = _county.GetAllReadOnly().ToList();
            var districts = _dist.GetAllReadOnly().ToList();
            var filterArea = new SearchFilterDto()
            {
                County = countys.Select(c=>c.CountyName).ToList(),
                Areas = districts.Select(d=>new AreaDto
                {
                    County= countys.First(c=>c.CountyId==d.CountyId).CountyName,
                    District=d.DistrictName
                }).ToList()
            };
            return filterArea;
        }
        #endregion

        #region 商品頁
        public ProductDto ProductDtoDetail(int productId)
        {
            var customerId = _accountManager.GetLoginMemberId();
            var product = GetProduct(productId);
            var getProduct = new ProductDto();
            if (product == null)
            {
                return null;
            }
            else if (product.IsDelete==true || product.ProductStatus== (int)ProductStatus.OffSale)
            {
                getProduct.State = product.ProductStatus;
                getProduct.IsDelete = product.IsDelete;
                return getProduct;
            }

            var sitter = GetSitter(product.SitterId);
            var member = _member.GetAllReadOnly().First(c => c.MemberId == sitter.MemberId);
            var pettype = GetPetType(productId);
            var evluation = GetEvaluationDapper(productId);
            var serviceType = GetServiceType(product.ServiceType);
            var areas = _area.GetAllReadOnly().Where(a => a.ProductId == productId).ToList();
            var countys = _county.GetAllReadOnly().ToList();
            var districts = _dist.GetAllReadOnly().ToList();
            getProduct = new ProductDto()
            {
                ProductId = product.ProductId,
                State=product.ProductStatus,
                Images = GetProductImageList(productId),
                SitterId= sitter.MemberId,
                SitterName = sitter.SitterName,
                SitterImg = sitter.SitterPicture ?? member.ProfileImage?? "/images/paw.png",
                SitterInfo = sitter.SitterInfo,
                ServiceType = serviceType.TypeName,
                NoticeIntroduce = serviceType.ServiceIntro,
                Introduce = product.Introduce,
                ServiceArea = GetServiceArea(areas, countys, districts),
                EvaluationAverage =(double) evluation[0].EvaluationAvg,
                OrderQuantity = GetOrderCount(productId),
                Collect = GetIsCollect(productId, customerId),
                Price = pettype.First().Price,
                Prices = GetPetTypePrice(productId, pettype),
                IsDelete=product.IsDelete
            };
            return getProduct;
        }
        public ProductDiscountDto DiscountDetail(int productId)
        {
            var discounts = _discount.GetAllReadOnly().FirstOrDefault(x=>x.ProductId==productId);     
            var discount = discounts!=null ? discounts:null;
            var getDiscount= new ProductDiscountDto();
            if (discount != null)
            {
                getDiscount = new ProductDiscountDto()
                {
                    Quantity = discount.Quantity,
                    Discount = discount.Discount
                };
            }
            else
            {
                getDiscount = new ProductDiscountDto()
                {
                    Quantity = 0,
                    Discount = 0
                };
            }
            return getDiscount;
        }
        public ProductChooseDto ChooseDetail(int productId)
        {
            var times = _time.GetAllReadOnly().Where(d => d.ProductId == productId).OrderBy(t => t.ServiceDay).OrderBy(t => t.ServicePartTime).ToList();
            var areas = GetArea(productId);
            var pettype = GetPetType(productId);
            var schedules = _schedule.GetAllReadOnly().ToList();
            var countys=_county.GetAllReadOnly().ToList();
            var districts = _dist.GetAllReadOnly().ToList();
            var getChoose = new ProductChooseDto()
            {
                Weekday = times.Select(d => d.ServiceDay).ToList(),
                Times = times.Select(d => new ChooseTimeDto
                {
                    Weekday = d.ServiceDay,
                    Time = d.ServicePartTime,
                    TimeTitle = EnumDisplayHelper.GetDisplayName<ServiceTime>((ServiceTime)d.ServicePartTime),
                    TimeDesrcipt = schedules.Where(s => s.PartTimeId == d.ServicePartTime).Select(s => s.TimeDesrcipt).ToList()
                }).ToList(),
                County = areas.Select(a => countys.First(c => c.CountyId == a.County).CountyName).ToList(),
                Areas = areas.Select(a => new ChooseAreaDto()
                {
                    County = countys.Where(c => c.CountyId == a.County).Select(c => c.CountyName).Distinct().First(),
                    District = districts.First(d => d.DistrictId == a.District).DistrictName
                }).ToList(),
                PetType = pettype.Select(t => EnumDisplayHelper.GetDisplayName<PetType>((PetType)t.PetType)).ToList(),
                Types = pettype.OrderBy(p => p.PetType).ThenBy(p=>p.ShapeType).Select(t => new ChoosePetTypeDto()
                {
                    PetType = EnumDisplayHelper.GetDisplayName<PetType>((PetType)t.PetType),
                    PetTypeId = t.PetType,
                    ShapeType = EnumDisplayHelper.GetDisplayName<ShapeType>((ShapeType)t.ShapeType),
                    ShapeTypeId = t.ShapeType,
                    Price=t.Price.ToString("#,###"),
                    OvernightPrice=t.OvernightPrice.ToString("#,###")
                }).ToList()
            };
            return getChoose;
        }
        public List<ProductEvaluationDto> EvaluationDetail(int productId)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            List<ProductEvaluationDto> evaluations = new List<ProductEvaluationDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                string evaluation = @"SELECT
	                                    e.Evaluation,
	                                    m.[Name] as Evaluator,
	                                    m.ProfileImage as EvaluatorImg,
	                                    e.[Message],
	                                    e.CreateTime as MessageTime
                                    FROM Product as p
                                    INNER JOIN [Order] as o ON  o.ProductID=p.ProductId
                                    INNER JOIN Evaluation as e ON e.OrderID = o.OrderID AND UserType='1'
                                    INNER JOIN Member as m ON m.MemberID=e.UserID
                                    WHERE p.IsDelete='False'  AND p.ProductStatus='0' AND p.ProductID=@Id";
                evaluations = connection.Query<ProductEvaluationDto>(evaluation, new { Id = productId }).ToList();
            }
            if (evaluations == null)
            {
                evaluations = new List<ProductEvaluationDto>();
                return evaluations;
            }
            return evaluations;
        }
        public ProductToCartDto SelectedDetail()
        {
            ProductToCartDto selected = new ProductToCartDto();
            return selected;
        }        

        public ResultDto GetDisabledTimeWebApi(int productId,int year, int month, int day)
        {
            var response = new ResultDto();
            try
            {
                var disabledtimes = new List<int>();
                var date= new DateTime(year,month,day);
                var orders = _order.GetAllReadOnly().Where(o => o.ProductId == productId && o.OrderStatus!= (int)OrderStatus.Cancel).Select(o=>o.OrderId).ToList();
                var orderschedules = _orderschedule.GetAllReadOnly().Where(t=> t.ServiceDate == date).ToList();
                foreach(var order in orders)
                {
                    var ordertimes = orderschedules.Where(t => t.OrderId == order).Select(t=>t.Schedule).ToList();
                    ordertimes.ForEach(x => disabledtimes.Add(x));
                }
                response.IsSuccess = true;
                response.Data = disabledtimes;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"失敗:{ex.Message}";
                return response;
            }
        }
        public ResultDto GetPriceWebApi(int productId, string types, string times)
        {
            var response = new ResultDto();
            var typeList = types.Split(",");
            var timesList = times.Split(",");
            try
            {
                var pettypes = _pettype.GetAllReadOnly().Where(x => x.ProductId == productId).ToList();
                decimal price = 0;
                decimal overnightPrice = 0;
                foreach (var type in typeList)
                {
                    var typeitem = type.Split("-");
                    var target = pettypes.Where(x => x.PetType == Int32.Parse(typeitem[0]) && x.ShapeType == Int32.Parse(typeitem[1])).Single();
                    price += target.Price;
                    overnightPrice += target.OvernightPrice;
                };
                var dayTimeQuantity = 0;
                var nightTimeQuantity = 0;
                var schedules = _schedule.GetAllReadOnly().ToList();
                foreach (var time in timesList)
                {
                    var schedule = schedules.First(s => s.ScheduleId == int.Parse(time)).PartTimeId;
                    if (schedule == 0)
                    {
                        nightTimeQuantity++;
                    }
                    else
                    {
                        dayTimeQuantity++;
                    }
                };
   
                var totalPrice = dayTimeQuantity * price + nightTimeQuantity * overnightPrice;

                response.IsSuccess = true;
                response.Data = totalPrice;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"失敗:{ex.Message}";
                return response;
            }
        }
        #endregion

        #region 新增購物車
        public ResultDto CreateCartWebApi(ProductToCartDto selected)
        {
            var response = new ResultDto();
            var date = selected.SelectedDay.Split("/");
            int year = Int32.Parse(date[0]);
            int month = Int32.Parse(date[1]);
            int day = Int32.Parse(date[2]);
            var times = selected.SelectedTime.Split(",");
            var types = selected.SelectedShapeTypes.Split(",");
            var countys=_county.GetAllReadOnly().ToList();
            var districts = _dist.GetAllReadOnly().ToList();
            var schedules = _schedule.GetAllReadOnly().ToList();
            try
            {
                var cart = new Cart
                {
                    ProductId = selected.SelectedId,
                    CustomerId = selected.MemberId,
                    CreateTime = DateTime.UtcNow,
                    County = countys.First(c => c.CountyName == selected.SelectedCounty).CountyId,
                    District = districts.First(d => d.DistrictName == selected.SelectedDistrict).DistrictId
                };
                _cart.Add(cart);
                foreach (var type in types)
                {
                    var typeitem = type.Split("-");
                    var cartdetail = new CartDetail
                    {
                        CartId = cart.CartId,
                        PetType = Int32.Parse(typeitem[0]),
                        ShapeType = Int32.Parse(typeitem[1])
                    };
                    _cartdetail.Add(cartdetail);
                };
                foreach (var time in times)
                {
                    var cartschedule = new CartSchedule
                    {
                        CartId = cart.CartId,
                        ServiceDate = new DateTime(year, month, day),
                        Schedule = int.Parse(time)
                    };
                    _cartschedule.Add(cartschedule);
                };
                return new ResultDto(selected);
            }
            catch (Exception ex)
            {
                response.Message = $"新增失敗:{ex.Message}";
                return response;
            }
        }
        #endregion

        #region 收藏CD
        public ResultDto CreateCollectWebApi([FromBody] CollectDto collect)
        {
            var response = new ResultDto();
            try
            {
                var getcollect = new Collect();
                var collected = _collect.GetAllReadOnly().Where(c => c.ProductId == collect.ProductId && c.MemberId == collect.MemberId).Any();
                if (collected == false)
                {
                    getcollect = new Collect
                    {
                        MemberId =collect.MemberId,
                        ProductId=collect.ProductId
                    };
                    _collect.Add(getcollect);
                }
                return new ResultDto(getcollect);
            }
            catch (Exception ex)
            {
                response.Message = $"新增失敗:{ex.Message}";
                return response;
            }
        }
        public ResultDto DeleteCollectWebApi([FromBody] CollectDto collect)
        {
            var response = new ResultDto();
            try
            {
                var getcollect = new Collect();
                var collected = _collect.GetAllReadOnly().Where(c => c.ProductId == collect.ProductId && c.MemberId == collect.MemberId).Any();
                if (collected == true)
                {
                    getcollect = new Collect
                    {
                        CollectId= _collect.GetAllReadOnly().First(c => c.ProductId == collect.ProductId && c.MemberId == collect.MemberId).CollectId
                    };
                    _collect.Delete(getcollect);
                }
                return new ResultDto(getcollect);
            }
            catch (Exception ex)
            {
                response.Message = $"刪除失敗:{ex.Message}";
                return response;
            }
        }
        #endregion

        #region 取資料
        private ApplicationCore.Entities.Product GetProduct(int productId)
        {
            var product = _product.GetById(productId);
            return product;
        }
        private List<string> GetProductImageList(int productId)
        {
            var images = _image.GetAllReadOnly().Where(x => x.ProductId == productId).OrderBy(x => x.Sort).Select(x => x.Image).ToList();
            return images;
        }
        private bool GetIsCollect(int productId, int customerId)
        {
            var collect = _collect.GetAllReadOnly().Any(c => c.ProductId == productId && c.MemberId == customerId);
            return collect;
        }
        private RegisterSitter GetSitter(int memberId)
        {
            var sitter = _sitter.GetAllReadOnly().First(x => x.MemberId == memberId);
            return sitter;
        }
        private ServiceType GetServiceType(int type)
        {
            var serviceType = _servicetype.GetAllReadOnly().First(x => x.ServiceTypeId == type);
            return serviceType;
        }
        private int GetOrderCount(int productId)
        {
            var count = _order.GetAllReadOnly().Where(x => x.ProductId == productId).Count();
            return count;
        }
        private List<Evaluation> GetEvaluationLsit(int productId)
        {
            var order = _order.GetAllReadOnly().FirstOrDefault(o => o.ProductId == productId);
            var evaluations = _evaluation.GetAllReadOnly();
            var evluation = new List<Evaluation>();
            if (order == null)
            {
                evluation = null;
            }
            else
            {
                evluation = evaluations.Where(e => e.OrderId ==
                                    order.OrderId && e.UserType == (int)UserType.Member).ToList();
            }
            return evluation;
        }
        private List<dynamic> GetEvaluationDapper(int productId)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:PawsDayConnection").Value;
            List<dynamic> evaluations = new List<dynamic>();
            using (var connection = new SqlConnection(connectionString))
            {
                string evaluation = @"SELECT
	                                    p.ProductId as ProductId,
	                                    COALESCE((SELECT
		                                    COUNT(e.Evaluation) as EvaluationCount
		                                    FROM Product as p2
		                                    INNER JOIN [Order] as o ON  o.ProductID=p2.ProductID
		                                    INNER JOIN Evaluation as e ON e.OrderID = o.OrderID AND UserType='1'
		                                    WHERE p2.ProductID=p.ProductID
		                                    GROUP BY p2.ProductID),'0') as EvaluationCount,
	                                    COALESCE((SELECT
		                                    CAST(AVG(e.Evaluation*1.0) AS decimal(9,1)) as EvaluationAVG
		                                    FROM Product as p2
		                                    INNER JOIN [Order] as o ON  o.ProductID=p2.ProductID
		                                    INNER JOIN Evaluation as e ON e.OrderID = o.OrderID AND UserType='1'
		                                    WHERE p2.ProductID=p.ProductID
		                                    GROUP BY p2.ProductID),'0') as EvaluationAvg
                                    FROM Product as p
                                    WHERE p.IsDelete='False'  AND p.ProductStatus='0' AND p.ProductID=@Id";
                 evaluations = connection.Query(evaluation, new { Id = productId }).ToList();
            }
            return evaluations;
        }
        private List<ProductServicePetType> GetPetType(int productId)
        {
            var pettype = _pettype.GetAllReadOnly().Where(t => t.ProductId == productId).ToList();
            return pettype;
        }
        private static List<decimal> GetPetTypePrice(int productId , List<ProductServicePetType> pettypeList)
        {
            var prices = new List<decimal>();
            foreach (var item in pettypeList)
            {
                var price = item.Price;
                prices.Add(price);
            }
            return prices;
        }
        private List<ProductServiceArea> GetArea(int productId)
        {
            var areas = _area.GetAllReadOnly().Where(a => a.ProductId == productId).ToList();
            return areas;
        }
        private static List<AreaDto> GetServiceArea(List<ProductServiceArea> areas,List<County> county,List<District> district)
        {
            var areaList = new List<AreaDto>();
            foreach (var item in areas)
            {
                var list = new AreaDto()
                {
                    County = county.First(c => c.CountyId == item.County).CountyName,
                    District = district.First(d => d.DistrictId == item.District).DistrictName
                };
                areaList.Add(list);
            }

            return areaList.Take(3).ToList();
        }

        #endregion
    }   
}

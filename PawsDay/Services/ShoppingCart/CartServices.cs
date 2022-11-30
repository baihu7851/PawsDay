using PawsDay.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Common;
using System.Linq;
using System.Security.Policy;
using System.ComponentModel.DataAnnotations;
using PawsDay.ViewModels.ShoppingCart.Carts;
using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using PawsDay.ViewModels.Product;
using PawsDay.Models.ShoppingCart.WebApi.BaseModel;
using System.Security.Cryptography;
using Infrastructure.Data;
using PawsDay.Models.ShoppingCart.WebApi.Enums;
using PawsDay.Services.Product;
using PawsDay.ViewModels.BecomePetsitter;
using isRock.LineBot;
using PawsDay.ViewModels.ShoppingCart.Booking.BookDTO;
using Microsoft.AspNetCore.Http;

namespace PawsDay.Services.ShoppingCart
{
    public class CartServices
    {
        

        private readonly IRepository<Member> _memberRepos;
        private readonly IRepository<Cart> _cartRepos;
        private readonly IRepository<CartDetail> _cartDetailRepos;
        private readonly IRepository<County> _countyRepos;
        private readonly IRepository<District> _distRepos;
        private readonly IRepository<ApplicationCore.Entities.Product> _productRepos;
        private readonly IRepository<ProductDiscount> _productDiscountRepos;

        private readonly IRepository<RegisterSitter> _sitterRepos;
        private readonly IRepository<ServiceType> _serviceRepos;
        private readonly IRepository<PetInfomation> _petInformation;
        private readonly IRepository<ProductImage> _productImageRepos;
        private readonly IRepository<CartSchedule> _cartScheduleRepos;
        private readonly IRepository<Schedule> _timeScheduleRepos;
        private readonly IRepository<ProductServicePetType> _productServicePetTypeRepos;
        private readonly IRepository<Collect> _collectRepos;
        private readonly IRepository<Order> _orderRepos;
        private readonly IRepository<OrderSchedule> _orderScheduleRepos;

        private readonly ShoppingCartRepository _shoppingCartRepository;

        private readonly HttpContext _httpContext;

        #region 相依注入
        public CartServices(
            IRepository<Member> memberRepos,
            IRepository<Cart> cartRepos,
            IRepository<County> countyRepos,
            IRepository<District> distRepos,
            IRepository<ApplicationCore.Entities.Product> productRepos,
            IRepository<RegisterSitter> sitterRepos,
            IRepository<ServiceType> serviceRepos,
            IRepository<PetInfomation> petInformation,
            IRepository<ProductImage> productImageRepos,
            IRepository<CartSchedule> cartScheduleRepos,
            IRepository<Schedule> timeScheduleRepos,
            IRepository<CartDetail> cartDetailRepos,
            IRepository<ProductServicePetType> productServicePetType,
            IRepository<Collect> collectRepos,
            IRepository<ProductDiscount> productDiscountRepos,
            ShoppingCartRepository shoppingCartRepository
,
            IRepository<Order> orderRepos,
            IRepository<OrderSchedule> orderScheduleRepos,
            IHttpContextAccessor httpContextAccessor)
        {
            _memberRepos = memberRepos;
            _cartRepos = cartRepos;
            _cartDetailRepos = cartDetailRepos;
            _cartScheduleRepos = cartScheduleRepos;
            _countyRepos = countyRepos;
            _distRepos = distRepos;
            _productRepos = productRepos;
            _sitterRepos = sitterRepos;
            _serviceRepos = serviceRepos;
            _petInformation = petInformation;
            _productImageRepos = productImageRepos;
            _timeScheduleRepos = timeScheduleRepos;
            _productServicePetTypeRepos = productServicePetType;
            _collectRepos = collectRepos;
            _productDiscountRepos = productDiscountRepos;
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepos = orderRepos;
            _orderScheduleRepos = orderScheduleRepos;
            _httpContext = httpContextAccessor.HttpContext;
        }
        #endregion

        #region 產生Cart的CartView
        public ListCartItemViewModel EstablishCartView(int userId)
        {
            var products = _productRepos.GetAllReadOnly();
            var orders = _orderRepos.GetAllReadOnly();
            var orderTimeSchedules = _orderScheduleRepos.GetAllReadOnly();
            var timeSchedules = _timeScheduleRepos.GetAllReadOnly();

            var cartItemsList = CreateListOfCartItem(userId);
            //
            var validItems = new List<CartItemViewModel>();
            var invalidItems = new List<CartItemViewModel>();
            //這邊判定失效條件

            foreach (var item in cartItemsList) 
            {
                /////先確認order的日期時間是否已經被占用
                bool timeIsExisted = false;
                /////需要用到productId, orderId, date, timeId
                int productId = item.ProductId;
                var itemDate = $"{item.ServiceDate}";
                

                var orderIds = (from o in orders
                                join time in orderTimeSchedules
                                on o.OrderId equals time.OrderId
                                where o.ProductId == productId && o.OrderStatus == (int)OrderStatus.Success
                                select o.OrderId).ToList(); //該productId擁有的orderId List
                var OrderedScheduleIds = new List<int>(); //已經被下單的時段
                foreach (var orderId in orderIds)
                {
                    var serviceDate = orderTimeSchedules.Where(x => x.OrderId == orderId).FirstOrDefault().ServiceDate.ToString("yyyy-MM-dd");
                    List<int> scheduleIds = orderTimeSchedules.Where(x => x.OrderId == orderId).Select(x=>x.Schedule).ToList();

                    if(serviceDate == itemDate)
                    {
                        foreach(int id in scheduleIds)
                        {
                            OrderedScheduleIds.Add(id);
                        }
                    }
                }
                OrderedScheduleIds = OrderedScheduleIds.Distinct().ToList();
                //開始找出Item的scheduleIds
                var itemTimeArray = item.ServiceTime.Split("~");
                string beginTimeStr = itemTimeArray[0].TrimEnd();
                string endTimeStr = itemTimeArray[1].TrimStart();
                var intBeginTime = timeSchedules.Where(x => x.TimeDesrcipt.Contains(beginTimeStr)).SingleOrDefault().ScheduleId;
                var intEndTime = timeSchedules.Where(x => x.TimeDesrcipt.Contains(endTimeStr)).SingleOrDefault().ScheduleId;

                var itemScheduleList = new List<int>();
                for(int i = intBeginTime; i<= intEndTime; i++)
                {
                    itemScheduleList.Add(i);
                }
                var result = itemScheduleList.Intersect(OrderedScheduleIds).ToList(); //確認是否有重複時段

                if (orderIds.Count == 0) //order資料庫找不到相同的產品+日期
                {
                    timeIsExisted = false;
                }
                else //如有找到，檢查time是否衝突
                {
                    if(result.Count != 0)
                    {
                        timeIsExisted = true;
                    }
                    else
                    {
                        timeIsExisted = false;
                    }
                }

                var serviceFullTime = DateTime.Parse($"{itemDate} {beginTimeStr}");




                var isValid = products.Where(p => p.ProductId == productId).SingleOrDefault().
                                ProductStatus == (int)ProductStatus.OnSale;

                
                //開始區分有效 or 失效cartItem

                if (DateTime.UtcNow.AddHours(8) > serviceFullTime.AddHours(-3)) //1. 時間過期
                //if (DateTime.Parse(item.ServiceDate) <= DateTime.UtcNow.AddHours(8).Date) //1. 時間過期
                {
                    invalidItems.Add(item);
                }
                
                else if (!isValid) //2. 產品下架
                {
                    invalidItems.Add(item);
                }
                else if (timeIsExisted)
                {
                    invalidItems.Add(item);
                }
                else
                {
                    validItems.Add(item);
                }


            }


            var listCartItems = new ListCartItemViewModel()
            {
                validCartItemList = validItems,
                expiredCartItemList = invalidItems,

            };

            return listCartItems;
        }
        #endregion

        #region 產生ListOfCartItem
        public List<CartItemViewModel> CreateListOfCartItem(int userId)
        {
            var carts = _cartRepos.GetAllReadOnly();
            var products = _productRepos.GetAllReadOnly();
            var productImages = _productImageRepos.GetAllReadOnly();
            var cartSchedules = _cartScheduleRepos.GetAllReadOnly();
            var timeSchedules = _timeScheduleRepos.GetAllReadOnly();
            var cartDetails = _cartDetailRepos.GetAllReadOnly();
            var services = _serviceRepos.GetAllReadOnly();
            var members = _memberRepos.GetAllReadOnly();
            var sitters = _sitterRepos.GetAllReadOnly();
            var collects = _collectRepos.GetAllReadOnly();
            var productDiscount = _productDiscountRepos.GetAllReadOnly();

            var productServicePetTypes = _productServicePetTypeRepos.GetAllReadOnly();
            //假設使用者Id

            var userCartList = new List<CartItemViewModel>(); //最後要回傳出使用者擁有購物車商品的List
            var currentTime = DateTimeOffset.UtcNow.DateTime;

            var cartIdList = carts.Where(x => x.CustomerId == userId).Select(x=>x.CartId).ToList(); //cartId可能為0, 1, or 多個
            foreach (var cartId in cartIdList)
            {
                //處理收藏
                var productId = carts.Where(c => c.CartId == cartId).Select(c => c.ProductId).SingleOrDefault();
                var isFavored = collects.Any(co => co.ProductId == productId);

                //處理照片
                var photo = productImages.Where(p => p.ProductId == productId && p.Sort == 1).Select(x => x.Image).SingleOrDefault();

                //處理時間
                var serviceDatetime = (from c in carts
                                      join sch in cartSchedules
                                      on c.CartId equals sch.CartId
                                      join t in timeSchedules
                                      on sch.Schedule equals t.ScheduleId
                                      where c.CartId == cartId
                                      orderby t.ScheduleId 
                                      select new
                                      {
                                          Date = sch.ServiceDate,
                                          Time = t.TimeDesrcipt
                                      }).ToList();


                var dateTime = serviceDatetime.FirstOrDefault();
                DateTime date;
                if(dateTime is null)
                {

                }
                date = dateTime.Date;

                var timeQuan = serviceDatetime.Count;
                var fullTimeDuration = string.Empty;
                if (timeQuan == 1)
                {
                    fullTimeDuration = serviceDatetime.SingleOrDefault().Time;
                }
                else
                {
                    var firstTime = serviceDatetime.Select(x => x.Time).ToList().First();//第一個時段的起始時間
                    var lastTime = serviceDatetime.Select(x => x.Time).ToList().Last();//最後時段的終止時間
                    fullTimeDuration = firstTime.Split('~')[0] + " ~ " + lastTime.Split('~')[1]; //連接整段期間
                }

                //寵物數量
                var petCount = (from ca in carts
                                join cd in cartDetails
                                on ca.CartId equals cd.CartId
                                where ca.CustomerId == userId && cd.CartId == cartId
                                select cd.CartDetailId).ToList().Count;

                //用時段unit處理discount
                
                var productDiscountDetail = (from pd in productDiscount
                                            where pd.ProductId == productId
                                            select pd).SingleOrDefault();


                decimal finalDiscount;

                if (productDiscountDetail is null) //沒有折扣方案
                {
                    finalDiscount = 1;
                }
                else //有折扣方案
                {
                    int productDiscountQuan = productDiscountDetail.Quantity;
                    decimal discountInt = productDiscount.Where(p => p.ProductId == productId).SingleOrDefault().Discount;

                    decimal productDiscountPercent = discountInt / 10;
                    //決定是否有折扣
                    finalDiscount = timeQuan >= productDiscountQuan ? productDiscountPercent : 1;
                }
                 
                //處理購物車每一隻寵物的 體型/種類/價格
                var petsList = (from detail in cartDetails
                                where detail.CartId == cartId
                                select new PetTitleDTO
                                {
                                    CartDetailId = detail.CartDetailId,
                                    PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)detail.PetType),
                                    PetTypeId = detail.PetType,
                                    ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)detail.ShapeType),
                                    ShapeTypeId = detail.ShapeType
                                }

                               ).ToList();

                //萬一，購物車存放的寵物types，已經不符合當下資料庫保母更新的寵物接受types
                foreach (var pet in petsList)
                {
                    var petType = pet.PetTypeId;
                    var shapeType = pet.ShapeTypeId;
                    var isValid = productServicePetTypes.Any(x => x.ProductId == productId && x.PetType == petType && x.ShapeType == shapeType);
                    if (!isValid)
                    {
                        petsList.Remove(pet);
                    }
                }

                //迴圈  拿cartId去當where條件  從Cart出發 join多張表格
                CartItemViewModel SingleCartItem = (from c in carts
                                      join p in products
                                      on c.ProductId equals p.ProductId
                                      join ser in services
                                      on p.ServiceType equals ser.ServiceTypeId

                                      where c.CartId == cartId

                                      select new CartItemViewModel()
                                      {
                                          CartId = c.CartId,
                                          ProductId = p.ProductId,
                                          Photo = photo,
                                          SitterName = (from ms in members
                                                        join sit in sitters
                                                        on p.SitterId equals sit.MemberId
                                                        where p.SitterId == sit.MemberId
                                                        select new { Name = sit.SitterName }).First().Name,
                                          Service = ser.TypeName,
                                          ServiceDate = date.ToString("yyyy-MM-dd"),
                                          ServiceTime = fullTimeDuration,
                                          NumberOfPets = petCount,
                                          Discount = finalDiscount,
                                          PetListHeader = petsList,
                                          
                                          SelectedTypeOptions = CreateSelectItems(cartId),
                                          IsFavored = isFavored


                                      }).FirstOrDefault();



                userCartList.Add(SingleCartItem);

            }

            return userCartList; //這是跟使用者相關的所有購物車商品
        }
        #endregion


        
    
        #region 產生訪客從Cookie取得CartView
        public ListCartItemViewModel CreateVisitorListOfCartItem(List<CookieCartDTO> source)
        {
            var orders = _orderRepos.GetAllReadOnly();
            var orderTimeSchedules = _orderScheduleRepos.GetAllReadOnly();
            var products = _productRepos.GetAllReadOnly();
            var productImages = _productImageRepos.GetAllReadOnly();
            var productDiscount = _productDiscountRepos.GetAllReadOnly();
            var schedules = _timeScheduleRepos.GetAllReadOnly();
            var services = _serviceRepos.GetAllReadOnly();
            var members = _memberRepos.GetAllReadOnly();
            var sitters = _sitterRepos.GetAllReadOnly();
            var timeSchedules = _timeScheduleRepos.GetAllReadOnly();

            var userCartList = new List<CartItemViewModel>();
            foreach (var item in source)
            {
                int productId = Int32.Parse(item.Id); //"10"
                string serviceDate = String.Join("-", item.Day.Split("/")); //"2022/10/20" => 2022-10-20
                

                //處理時間
                string serviceTime = item.Time; //"29,30,31"


                List<int> serviceDatetime = serviceTime.Split(",").Select(x => Int32.Parse(x)).ToList();
                var timeQuan = serviceDatetime.Count;
                var fullTimeDuration = string.Empty;
                if (timeQuan == 1)
                {
                    fullTimeDuration = timeSchedules.Where(x=>x.ScheduleId == serviceDatetime.FirstOrDefault()).SingleOrDefault().TimeDesrcipt;
                }
                else
                {
                    var firstTime = timeSchedules.Where(x=>x.ScheduleId == serviceDatetime.FirstOrDefault()).SingleOrDefault().TimeDesrcipt;//第一個時段的起始時間
                    var lastTime = timeSchedules.Where(x=>x.ScheduleId == serviceDatetime.LastOrDefault()).SingleOrDefault().TimeDesrcipt;//第一個時段的結束時間

                    fullTimeDuration = firstTime.Split('~')[0] + " ~ " + lastTime.Split('~')[1]; //連接整段期間
                }



                string city = item.County;
                string dist = item.District;


                //處理寵物
                List<string> Types = item.ShapeTypes.Split(",").ToList(); //"0-2,0-0,0-1"
                var petCount = Types.Count;
                List<PetTitleDTO> petsList = new List<PetTitleDTO>();
                foreach (var pet in Types)
                {
                    string[] combo = pet.Split("-");
                    int petType = Int32.Parse(combo[0]);
                    int shapeType = Int32.Parse(combo[1]);
                    var petDTO = new PetTitleDTO()
                    {
                        PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)petType),
                        PetTypeId = petType,
                        ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)shapeType),
                        ShapeTypeId = shapeType,
                    };
                    petsList.Add(petDTO);
                }

                //Photo
                var photo = productImages.Where(p => p.ProductId == productId && p.Sort == 1).Select(x => x.Image).SingleOrDefault();

                //用時段unit處理discount

                var productDiscountDetail = (from pd in productDiscount
                                             where pd.ProductId == productId
                                             select pd).SingleOrDefault();


                decimal finalDiscount;

                if (productDiscountDetail is null) //沒有折扣方案
                {
                    finalDiscount = 1;
                }
                else //有折扣方案
                {
                    int productDiscountQuan = productDiscountDetail.Quantity;
                    decimal discountInt = productDiscount.Where(p => p.ProductId == productId).SingleOrDefault().Discount;

                    decimal productDiscountPercent = discountInt / 10;
                    //決定是否有折扣
                    finalDiscount = timeQuan >= productDiscountQuan ? productDiscountPercent : 1;
                }


                CartItemViewModel SingleCartItem = (from p in products
                                                    join ser in services
                                                    on p.ServiceType equals ser.ServiceTypeId

                                                    where p.ProductId == productId

                                                    select new CartItemViewModel()
                                                    {
                                                        //CartId = c.CartId,
                                                        ProductId = p.ProductId,
                                                        Photo = photo,
                                                        SitterName = (from ms in members
                                                                      join sit in sitters
                                                                      on p.SitterId equals sit.MemberId
                                                                      where p.SitterId == sit.MemberId
                                                                      select new { Name = sit.SitterName }).First().Name,
                                                        CartCityName= city,
                                                        CartDistrictName = dist,

                                                        Service = ser.TypeName,
                                                        ServiceDate = serviceDate,
                                                        ServiceTime = fullTimeDuration,
                                                        ScheduleIdsStr = serviceTime,  //"29,30,31"
                                                        NumberOfPets = petCount,
                                                        Discount = finalDiscount,
                                                        PetListHeader = petsList,
                                                        SelectedTypeOptions = CreateVisitorSelectItems(productId),
                                                        


                                                    }).FirstOrDefault();



                userCartList.Add(SingleCartItem);






            }


            //
            var validItems = new List<CartItemViewModel>();
            var invalidItems = new List<CartItemViewModel>();
            //這邊判定失效條件

            foreach (var item in userCartList)
            {
                /////先確認order的日期時間是否已經被占用
                bool timeIsExisted = false;
                /////需要用到productId, orderId, date, timeId
                int productId = item.ProductId;
                var itemDate = $"{item.ServiceDate}";


                var orderIds = (from o in orders
                                join time in orderTimeSchedules
                                on o.OrderId equals time.OrderId
                                where o.ProductId == productId
                                select o.OrderId).ToList(); //該productId擁有的orderId List
                var OrderedScheduleIds = new List<int>(); //已經被下單的時段
                foreach (var orderId in orderIds)
                {
                    var serviceDate = orderTimeSchedules.Where(x => x.OrderId == orderId).FirstOrDefault().ServiceDate.ToString("yyyy-MM-dd");
                    List<int> scheduleIds = orderTimeSchedules.Where(x => x.OrderId == orderId).Select(x => x.Schedule).ToList();

                    if (serviceDate == itemDate)
                    {
                        foreach (int id in scheduleIds)
                        {
                            OrderedScheduleIds.Add(id);
                        }
                    }
                }
                OrderedScheduleIds = OrderedScheduleIds.Distinct().ToList();
                //開始找出Item的scheduleIds
                var itemTimeArray = item.ServiceTime.Split("~");
                string beginTimeStr = itemTimeArray[0].TrimEnd();
                string endTimeStr = itemTimeArray[1].TrimStart();
                var intBeginTime = timeSchedules.Where(x => x.TimeDesrcipt.Contains(beginTimeStr)).SingleOrDefault().ScheduleId;
                var intEndTime = timeSchedules.Where(x => x.TimeDesrcipt.Contains(endTimeStr)).SingleOrDefault().ScheduleId;

                var itemScheduleList = new List<int>();
                for (int i = intBeginTime; i <= intEndTime; i++)
                {
                    itemScheduleList.Add(i);
                }
                var result = itemScheduleList.Intersect(OrderedScheduleIds).ToList();

                if (orderIds.Count == 0) //order資料庫找不到相同的產品+日期
                {
                    timeIsExisted = false;
                }
                else //如有找到，檢查time是否衝突
                {
                    if (result.Count != 0)
                    {
                        timeIsExisted = true;
                    }
                    else
                    {
                        timeIsExisted = false;
                    }
                }




                var isValid = products.Where(p => p.ProductId == productId).SingleOrDefault().
                                ProductStatus == (int)ProductStatus.OnSale;


                if (DateTime.Parse(item.ServiceDate) <= DateTime.UtcNow.AddHours(8).Date) //1. 時間過期
                {
                    invalidItems.Add(item);
                }

                else if (!isValid) //2. 產品下架
                {
                    invalidItems.Add(item);
                }
                else if (timeIsExisted)
                {
                    invalidItems.Add(item);
                }
                else
                {
                    validItems.Add(item);
                }


            }


            var listCartItems = new ListCartItemViewModel()
            {
                validCartItemList = validItems,
                expiredCartItemList = invalidItems,

            };

            return listCartItems;



        }
        #endregion

        #region 訪客產生下拉選單
        public CreateSelectedDTO CreateVisitorSelectItems(int productId)
        {

            var petOptions = FindVisitorSitterServiceOptions(productId);

            var petTypeList = petOptions.PetTypes.Select(x => x).ToList();

            var shapeTypeList = petOptions.TypeOptions.Select(x => new ChoosePetTypeDto()
            {
                PetType = x.PetType,
                ShapeType = x.ShapeType
            }).ToList();

            var serviceTypes = new CreateSelectedDTO()
            {
                ServicePetTypes = petTypeList,
                ServiceShapeTypes = shapeTypeList
            };

            return serviceTypes;
        }
        #endregion

        #region 訪客找保姆接受寵物選項
        public SitterOptionsDTO FindVisitorSitterServiceOptions(int productId)
        {
            var productServicePetTypes = _productServicePetTypeRepos.GetAll();
            var carts = _cartRepos.GetAll();
            
            
            var petvalidList = productServicePetTypes.Where(x => x.ProductId == productId).Select(x => x.PetType).Distinct();
            //Console.WriteLine(petvalidList.Count());
            var shapesDTOList = productServicePetTypes.Where(x => petvalidList.Contains(x.PetType) && x.ProductId == productId)

                .Select(x => new ChoosePetTypeDto
                {
                    //CartDetailId = cartId, //
                    PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)x.PetType),
                    ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)x.ShapeType)

                }).ToList();

            var sitterOptions = new SitterOptionsDTO()
            {
                PetTypes = petvalidList.Select(x => EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)x)).ToList(),
                TypeOptions = shapesDTOList
            };

            return sitterOptions;
        }
        #endregion

        #region 登入產生下拉選單
        public CreateSelectedDTO CreateSelectItems(int cartId)
        {

            var petOptions = FindSitterServiceOptions(cartId);

            var petTypeList = petOptions.PetTypes.Select(x => x).ToList();

            var shapeTypeList = petOptions.TypeOptions.Select(x => new ChoosePetTypeDto()
            {
                PetType = x.PetType,
                ShapeType = x.ShapeType,
                ShapeTypeId = x.ShapeTypeId
            }).OrderBy(x=>x.ShapeTypeId).ToList();

            var serviceTypes = new CreateSelectedDTO()
            {
                ServicePetTypes = petTypeList,
                ServiceShapeTypes = shapeTypeList
            };

            return serviceTypes;
        }
        #endregion

        #region 登入找保姆接受寵物選項
        public SitterOptionsDTO FindSitterServiceOptions(int cartId)
        {
            var productServicePetTypes = _productServicePetTypeRepos.GetAll();
            var carts = _cartRepos.GetAll();
            var productId = carts.FirstOrDefault(x => x.CartId == cartId).ProductId;
            //var productId = 1;
            var petvalidList = productServicePetTypes.Where(x => x.ProductId == productId).Select(x=> x.PetType).Distinct();
            //Console.WriteLine(petvalidList.Count());
            var shapesDTOList = productServicePetTypes.Where(x => petvalidList.Contains(x.PetType) && x.ProductId == productId)
                
                .Select(x => new ChoosePetTypeDto
                {
                    //CartDetailId = cartId, //
                    PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)x.PetType),
                    ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)x.ShapeType)

                }).ToList();

            var sitterOptions = new SitterOptionsDTO()
            {
                PetTypes = petvalidList.Select(x=> EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)x)).ToList(),
                TypeOptions = shapesDTOList
            };

            return sitterOptions;
        }
        #endregion



        #region 從商品頁產生結帳商品
        public CartItemViewModel FromProductCreateCartItem(ProductToCartDto product)
        {
            
            var products = _productRepos.GetAll();
            var productImages = _productImageRepos.GetAll();
            var sitters = _sitterRepos.GetAll();
            var services = _serviceRepos.GetAll();
            var petInfos = _petInformation.GetAll();
            var productDiscount = _productDiscountRepos.GetAllReadOnly();
            var timeSchedules = _timeScheduleRepos.GetAllReadOnly();

            var productId = product.SelectedId;
            //CartItemViewModel cart = new CartItemViewModel();
                //date
            string serviceDate = product.SelectedDay;
            serviceDate = String.Join("-",serviceDate.Split('/'));

                //time
            string serviceAllTimes = product.SelectedTime; //"3,4,5"
            var timeList = serviceAllTimes.Split(',').ToList();  
            var serviceTime = string.Empty;
            var timeSpan = timeList.Count;

            var timeStringList = new List<string>();
            foreach (string time in timeList)
            {
                var intTime = int.Parse(time);
                var timeString = timeSchedules.Where(x => x.ScheduleId == intTime).SingleOrDefault().TimeDesrcipt;
                timeStringList.Add(timeString);
            }



            if (timeSpan == 1)
            {
                serviceTime = timeStringList[0];
            }
            else
            {
                var beginTime = timeStringList.First().Split('~')[0];
                var endTime = timeStringList.Last().Split('~')[1];
                serviceTime = beginTime + " ~ " + endTime;
            }
            
            

            var city = product.SelectedCounty;
            var dist = product.SelectedDistrict;

            ///以下找出product的東西
                //Photo
            var photo = (from ima in productImages
                        where ima.ProductId == productId && ima.Sort == 1
                        select ima.Image).SingleOrDefault();
            //SitterName
            var sitterName = (from p in products
                              join si in sitters
                              on p.SitterId equals si.MemberId
                             where p.ProductId == productId
                              select si.SitterName).SingleOrDefault();

            //Service
            var serviceType = (from p in products
                            join s in services
                            on p.ServiceType equals s.ServiceTypeId
                            where p.ProductId == productId
                               select s.TypeName).SingleOrDefault();

            //Pet
            var petRawData = product.SelectedShapeTypes;
            var listOfPets = petRawData.Split(',').ToList();
            List<PetTitleDTO> petList = new List<PetTitleDTO>();

            foreach (var petCombo in listOfPets)
            {
                int petTypeId = int.Parse(petCombo.Split('-')[0]);
                int shapeTypeId = int.Parse(petCombo.Split('-')[1]);
                string petType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)petTypeId);
                string shapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)shapeTypeId);
                var petDTO = new PetTitleDTO()
                {
                    PetType = petType,
                    ShapeType = shapeType
                };
                petList.Add(petDTO);
            }
            var numberOfPet = petList.Count;

            //用時段unit處理discount
            var productDiscountDetail = productDiscount.Where(p => p.ProductId == productId).SingleOrDefault();

            int price = (int)(decimal.Parse(product.SelectedPrice));

            decimal finalDiscount;

            if (productDiscountDetail is null) //沒有折扣方案
            {
                finalDiscount = 1;
            }
            else //有折扣方案
            {
                int productDiscountQuan = productDiscountDetail.Quantity;
                decimal discountInt = productDiscount.Where(p => p.ProductId == productId).SingleOrDefault().Discount;

                decimal productDiscountPercent = discountInt / 10;
                //決定是否有折扣
                finalDiscount = timeSpan >= productDiscountQuan ? productDiscountPercent : 1;
            }


            ///

            CartItemViewModel cart = new CartItemViewModel()
                  {

                        ProductId = productId,
                        Photo = photo,
                        SitterName = sitterName,
                        Service = serviceType,
                        Discount = finalDiscount,
                        ServiceDate = serviceDate,
                        ServiceTime = serviceTime,
                        TimeSpan = timeSpan,
                        CartCityName = city,
                        CartDistrictName = dist,


                        NumberOfPets = numberOfPet,

                      PetListHeader = petList,
                      FinalCartPrice = price,
                  };




            return cart;
        }
        #endregion




        public MemberInfoDTO GetMemberInfo(int userId)
        {
            var members = _memberRepos.GetAllReadOnly();

            var member = (from m in members
                              where m.MemberId == userId
                              select new MemberInfoDTO()
                              {
                                  MemberName = m.Name,
                                  MemberTel = m.Phone,
                                  MemberEmail = m.Email,
                                  MemberAddress = m.Address

                              }).SingleOrDefault();

            

            return member;
        }

        public string GetValue(string key)
        {
            var value = "";
            _httpContext.Request.Cookies.TryGetValue(key, out value);
            if (string.IsNullOrWhiteSpace(value))
            {
                value = string.Empty;
            }
            return value;
        }

        #region 綠界API打後，刪除購物車項目
        public void DeletePaidItems(IEnumerable<string> cartIds)
        {
            _shoppingCartRepository.DeleteCompleteCart(cartIds);
        }
        #endregion

        #region API Area

        public BaseResult UpdateCartItem(List<CartDetailDTO> sourceList)
        {
            var cartDetails = _cartDetailRepos.GetAll();
            var response = new BaseResult();
            //查詢cartDeail資料庫中，符合傳入資料cartId的
            var cartDetail = cartDetails.Where(x => x.CartId == sourceList.FirstOrDefault().CartId).ToList();
            
            try 
            {
                if (cartDetail is null)
                {
                    throw new Exception();
                }
                foreach (var item in cartDetail)
                {
                    foreach (var requestItem in sourceList)
                    {
                        if (requestItem.CartDetailId == item.CartDetailId)
                        {
                            item.PetType = requestItem.PetType;
                            item.ShapeType = requestItem.ShapeType;
                            
                        }
                    }
                }
                _cartDetailRepos.UpdateRange(cartDetail);
                response.IsSuccess = true;
                response.Response = ApiStatus.Success;
                return response;
            }
            catch(Exception err)
            {
                response.IsSuccess = false;
                response.Response = ApiStatus.NotFound;
                response.Message = err.ToString();
                return response;
            }
        }
        
        public BaseResult GetProductUnitPrice(int cartId, int productId, int petType, int shapeType)
        {
            var productServicePetTypes = _productServicePetTypeRepos.GetAll();
            var carts = _cartRepos.GetAll();
            var cartSchedules = _cartScheduleRepos.GetAll();
            var timeSchedules = _timeScheduleRepos.GetAll();

            var response = new BaseResult();

            //第二版本
            //target: ProductServicePetType型別
            var target = productServicePetTypes.Where(x => x.ProductId == productId && x.PetType == petType && x.ShapeType == shapeType).SingleOrDefault();

            try
            {
                if (target is null)
                {
                    throw new Exception();
                }

                //step2 用CartID去CartSchedule找 一對多 的ScheduleIDs(List)，這些是cartItem所擁有的ScheduleID(時段)
                var productScheduleIds = cartSchedules.Where(x => x.CartId == cartId).Select(x => x.Schedule).ToList();

                //step3 從Schedule找出 PartTimeID ==3 , select ScheduleIDs(List) 這些是夜間的ScheduleID
                var nightScheduleIDs = timeSchedules.Where(x => x.PartTimeId == (int)ServiceTime.Aftermidnight).Select(x => x.ScheduleId).ToList();

                var listOfDayTime = new List<int>();
                var listOfNightTime = new List<int>();

                foreach (var productTime in productScheduleIds)
                {
                    var IsNight = nightScheduleIDs.Any(x => x == productTime);
                    if (!IsNight)
                    {
                        listOfDayTime.Add(productTime);
                    }
                    else
                    {
                        listOfNightTime.Add(productTime);
                    }
                }
                var dayTimeQuantity = listOfDayTime.Count;
                var nightTimeQuantity = listOfNightTime.Count;
                
                var totalPrice = dayTimeQuantity * target.Price + nightTimeQuantity * target.OvernightPrice;

                response.IsSuccess = true;
                response.Response = ApiStatus.Success;
                response.Body = totalPrice;
                return response;
            }
            catch (Exception err)
            {
                response.IsSuccess = false;
                response.Response = ApiStatus.NotFound;
                response.Message = err.ToString();
                return response;
            }


        }

        public BaseResult VisitorGetProductUnitPrice(string timeString, int productId, int petType, int shapeType)
        {
            var productServicePetTypes = _productServicePetTypeRepos.GetAll();
            var carts = _cartRepos.GetAll();
            var cartSchedules = _cartScheduleRepos.GetAll();
            var timeSchedules = _timeScheduleRepos.GetAll();

            var response = new BaseResult();

            //第二版本
            //target: ProductServicePetType型別
            var target = productServicePetTypes.Where(x => x.ProductId == productId && x.PetType == petType && x.ShapeType == shapeType).SingleOrDefault();

            try
            {
                if (target is null)
                {
                    throw new Exception();
                }

                //step2 用CartID去CartSchedule找 一對多 的ScheduleIDs(List)，這些是cartItem所擁有的ScheduleID(時段)
                
                List<int> productScheduleIds = timeString.Split(",").Select(x=>Int32.Parse(x)).ToList();

                //step3 從Schedule找出 PartTimeID ==3 , select ScheduleIDs(List) 這些是夜間的ScheduleID
                var nightScheduleIDs = timeSchedules.Where(x => x.PartTimeId == (int)ServiceTime.Aftermidnight).Select(x => x.ScheduleId).ToList();

                var listOfDayTime = new List<int>();
                var listOfNightTime = new List<int>();

                foreach (var productTime in productScheduleIds)
                {
                    var IsNight = nightScheduleIDs.Any(x => x == productTime);
                    if (!IsNight)
                    {
                        listOfDayTime.Add(productTime);
                    }
                    else
                    {
                        listOfNightTime.Add(productTime);
                    }
                }
                var dayTimeQuantity = listOfDayTime.Count;
                var nightTimeQuantity = listOfNightTime.Count;

                var totalPrice = dayTimeQuantity * target.Price + nightTimeQuantity * target.OvernightPrice;

                response.IsSuccess = true;
                response.Response = ApiStatus.Success;
                response.Body = totalPrice;
                return response;
            }
            catch (Exception err)
            {
                response.IsSuccess = false;
                response.Response = ApiStatus.NotFound;
                response.Message = err.ToString();
                return response;
            }


        }

        public BaseResult DeleteSelectedItem(string strCartIds)
        {
            var response = new BaseResult();
            

            var carts = _cartRepos.GetAll();
            var cartSchedules = _cartScheduleRepos.GetAll();
            var cartDetails = _cartDetailRepos.GetAll();

            List<int> cartIds = strCartIds.Split(",").Select(x => Int32.Parse(x)).ToList();


            try 
            {
                foreach(var cartId in cartIds)
                {

                    var targetCart = carts.Where(c => c.CartId == cartId).ToList().SingleOrDefault();
                    if (targetCart is null)
                    {
                        throw new Exception();
                    }

                    var targetCartDetail = cartDetails.Where(c => c.CartId == cartId).ToList();
                    var targetCartSchedule = cartSchedules.Where(c => c.CartId == cartId).ToList();
                    _cartDetailRepos.DeleteRange(targetCartDetail);
                    _cartScheduleRepos.DeleteRange(targetCartSchedule);
                    _cartRepos.Delete(targetCart);
                    
                }
                response.IsSuccess = true;
                response.Response = ApiStatus.Success;
                response.Body = "刪除成功!";
                return response;

            }
            catch (Exception err)
            {
                response.IsSuccess = false;
                response.Response = ApiStatus.NotFound;
                response.Body = "刪除失敗!";
                response.Message = err.ToString();
                return response;
            }

            


            
        }
        #endregion

    }
}

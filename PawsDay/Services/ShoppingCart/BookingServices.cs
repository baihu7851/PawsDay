using PawsDay.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;
using ApplicationCore.Common;
using System.ComponentModel.DataAnnotations;
using PawsDay.ViewModels.ShoppingCart.Carts;
using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PawsDay.ViewModels.ShoppingCart.Booking;
using System.Web;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using Infrastructure.Services;
using System.Runtime.Intrinsics.Arm;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced.DTO;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.Encodings.Web;
using isRock.LineBot;
using Microsoft.Extensions.Configuration;
using isRock.Toolbox;
using System.Net.Http.Headers;

namespace PawsDay.Services.ShoppingCart
{
    public class BookingServices
    {
        //private readonly IHttpClientFactory _clientFactory;
        private readonly IConfigurationSection _returnUrlConfig, _resultUrlConfig, _NewebResultURL;

        private readonly IRepository<Member> _memberRepos;
        private readonly IRepository<Cart> _cartRepos;
        private readonly IRepository<County> _countyRepos;
        private readonly IRepository<District> _distRepos;
        private readonly IRepository<ApplicationCore.Entities.Product> _productRepos;
        private readonly IRepository<RegisterSitter> _sitterRepos;
        private readonly IRepository<ServiceType> _serviceRepos;
        private readonly IRepository<PetInfomation> _petInformation;
        private readonly IRepository<ProductImage> _productImageRepos;
        private readonly IRepository<CartDetail> _cartDetailRepos;
        private readonly IRepository<Schedule> _timeScheduleRepos;
        private readonly IRepository<CartSchedule> _cartScheduleRepos;

        private readonly IRepository<Order> _orderRepos;
        private readonly IRepository<OrderSchedule> _orderScheduleRepos;
        private readonly IRepository<OrderPetDetail> _orderPetDetailRepos;
        private readonly IRepository<ProductServicePetType> _productServicePetTypeRepos;

        //HttpHost
        private readonly HttpContext _httpContext;

        public BookingServices(IRepository<Member> memberRepos, IRepository<Cart> cartRepos, IRepository<County> countyRepos, IRepository<District> distRepos, IRepository<ApplicationCore.Entities.Product> productRepos, IRepository<RegisterSitter> sitterRepos, IRepository<ServiceType> serviceRepos, IRepository<PetInfomation> petInformation, IRepository<ProductImage> productImageRepos, IRepository<CartDetail> cartDetailRepos, IRepository<Schedule> timeScheduleRepos, IRepository<CartSchedule> cartScheduleRepos, IRepository<OrderSchedule> orderScheduleRepos, IRepository<OrderPetDetail> orderPetDetailRepos, IRepository<Order> orderRepos, IRepository<ProductServicePetType> productServicePetTypeRepos, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _memberRepos = memberRepos;
            _cartRepos = cartRepos;
            _countyRepos = countyRepos;
            _distRepos = distRepos;
            _productRepos = productRepos;
            _sitterRepos = sitterRepos;
            _serviceRepos = serviceRepos;
            _petInformation = petInformation;
            _productImageRepos = productImageRepos;
            _cartDetailRepos = cartDetailRepos;
            _timeScheduleRepos = timeScheduleRepos;
            _cartScheduleRepos = cartScheduleRepos;

            _orderRepos = orderRepos;
            _orderScheduleRepos = orderScheduleRepos;
            _orderPetDetailRepos = orderPetDetailRepos;
            _productServicePetTypeRepos = productServicePetTypeRepos;
            _httpContext = httpContextAccessor.HttpContext;
            _returnUrlConfig = config.GetSection("Payment:ReturnURL");
            _resultUrlConfig = config.GetSection("Payment:ResultURL");
            _NewebResultURL = config.GetSection("Payment:NewebResultURL");

            //_clientFactory = clientFactory;//傳送資料的物件
        }


        #region cart傳資料在booking渲染畫面
        public BookingInformationViewModel CreateBookingInformation(int userId, List<int> cartIds, List<decimal> prices, List<decimal> discounts)
        {
            var carts = _cartRepos.GetAll();
            var products = _productRepos.GetAll();
            var productImages = _productImageRepos.GetAll();
            var cartSchedules = _cartScheduleRepos.GetAll();
            var timeSchedules = _timeScheduleRepos.GetAll();
            var cartDetails = _cartDetailRepos.GetAll();
            var members = _memberRepos.GetAll();
            var countys = _countyRepos.GetAll();
            var dists = _distRepos.GetAll();
            var services = _serviceRepos.GetAll();
            var sitters = _sitterRepos.GetAll();
            var petInfos = _petInformation.GetAll();


            //var bookingViewModel = new BookingInformationViewModel();

            //下方開始處理booking裏頭有幾張cartItem
            var cartIdList = cartIds; //cartId可能為0, 1, or 多個

            var userCartItemList = new List<CartItemViewModel>(); //要放進BookingVM的cartItem清單

            int count = 0;
            foreach (var cartId in cartIdList)
            {
                var price = prices[count];
                var intPrice = (int)price;
                var discount = discounts[count];

                //處理照片
                var photo = (from c in carts
                             join ima in productImages
                             on c.ProductId equals ima.ProductId
                             where c.CartId == cartId && ima.Sort == 1
                             select ima.Image).First();
                //處理時間
                var serviceDatetime = from c in carts
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
                                      };
                var firstTime = serviceDatetime.Select(x => x.Time).ToList().First();//第一個時段的起始時間
                var lastTime = serviceDatetime.Select(x => x.Time).ToList().Last();//最後時段的終止時間
                var fullTimeDuration = firstTime.Split('~')[0] + " ~ " + lastTime.Split('~')[1]; //連接整段期間

                //處理購物車每一隻寵物的 體型/種類/價格
                var petsTitleList = (from detail in cartDetails
                                     where detail.CartId == cartId
                                     select new PetTitleDTO
                                     {
                                         PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)detail.PetType),
                                         ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)detail.ShapeType),
                                         //Price = detail.UnitPrice * detail.Quantity
                                     }

                                   ).ToList();


                //組一個商品的地方
                //迴圈  拿cartId去當where條件  從Cart出發 join多張表格
                var SingleCartItem = (from c in carts
                                      join p in products
                                      on c.ProductId equals p.ProductId
                                      join ser in services
                                      on p.ServiceType equals ser.ServiceTypeId
                                      //join m in members
                                      //on userId equals m.MemberId

                                      where c.CartId == cartId

                                      select new CartItemViewModel()
                                      {
                                          ProductId = p.ProductId,
                                          CartId = c.CartId,
                                          Photo = photo,
                                          SitterName = (from ms in members
                                                        join sit in sitters
                                                        on p.SitterId equals sit.MemberId
                                                        where p.SitterId == sit.MemberId
                                                        select new { Name = sit.SitterName }).First().Name,
                                          Service = ser.TypeName,
                                          ServiceDate = serviceDatetime.First().Date.ToString("yyyy-MM-dd"),
                                          ServiceTime = fullTimeDuration,
                                          NumberOfPets = (from ca in carts
                                                          join cd in cartDetails
                                                          on ca.CartId equals cd.CartId
                                                          where ca.CustomerId == userId && cd.CartId == c.CartId
                                                          select new { cartDetailId = cd.CartDetailId }).ToList().Count,

                                          PetListHeader = petsTitleList,

                                          //SetPetTypes = CreateSelectItems().SetPetTypes, //可能用不到
                                          //SetShapeTypes = CreateSelectItems().SetShapeTypes, //可能用不到
                                          CartCity = c.County,
                                          CartCityName = (from cit in countys
                                                          where cit.CountyId == c.County
                                                          select cit.CountyName).Single(),

                                          CartDistrict = c.District,
                                          CartDistrictName = (from dis in dists
                                                              where dis.DistrictId == c.District
                                                              select dis.DistrictName).Single(),
                                          //MemberAddress = m.Address,
                                          //FullAddress = fullAddress,
                                          FinalCartPrice = intPrice,
                                          Discount = discount,


                                      }).FirstOrDefault();

                userCartItemList.Add(SingleCartItem);

                count++;

            }

            //查詢會員預設擁有的寵物清單
            var memberPetList = petInfos.Where(x => x.MemberId == userId).ToList();

            //最後組出bookingVM的地方
            BookingInformationViewModel bookingViewModel = (from m in members
                                    join c in carts
                                    on m.MemberId equals c.CustomerId
                                    where m.MemberId == userId
                                    select new BookingInformationViewModel()
                                    {
                                        MemberName = m.Name,
                                        MemberTel = m.Phone,
                                        MemberEmail = m.Email,
                                        MemberAddress = m.Address,

                                        CartItemList = userCartItemList,
                                        MemberPetsList = memberPetList,

                                    })
                                    .FirstOrDefault();

            return bookingViewModel;
        }

        #endregion

        #region 商品頁面直接結帳
        [HttpPost]
        public BookingInformationViewModel FromProductToBooking(ProductToCartDto selected)
        {
            return new BookingInformationViewModel();
        }
        #endregion

        #region Booking傳資料，得到orderIDs
        public List<int> SavingBookingInformation(OrderPlacedViewModel source, int userId)
        {
            List<int> orderIdsProduced = new List<int>(); //此方法回傳orderIds的List

            //資料庫
            var carts = _cartRepos.GetAll();
            var products = _productRepos.GetAll();
            var productImages = _productImageRepos.GetAll();
            var cartSchedules = _cartScheduleRepos.GetAll();
            var timeSchedules = _timeScheduleRepos.GetAll();
            var cartDetails = _cartDetailRepos.GetAll();
            var members = _memberRepos.GetAll();
            var countys = _countyRepos.GetAll();
            var dists = _distRepos.GetAll();
            var services = _serviceRepos.GetAll();
            var sitters = _sitterRepos.GetAll();
            var petInfos = _petInformation.GetAll();
            var productServicePetTypes = _productServicePetTypeRepos.GetAll();
            var orders = _orderRepos.GetAllReadOnly();
            var orderTimeSchedules = _orderScheduleRepos.GetAllReadOnly();


            //共同項目
            string bookName = source.BookerName;
            string bookPhone = source.BookerTel;
            string bookEmail = source.BookerEmail;
            string invoiceCompanyTitle = source.CompanyTitleName;
            string invoiceCompanyTaxID = source.CompanyTaxID;



            int invoiceType = invoiceCompanyTitle is null || invoiceCompanyTaxID is null ? 1 : 2;


            //裏頭幾項商品
            List<CartItemViewModel> orderItemList = source.ListOfPayItems;
            if (orderItemList.Count == 0)
            {
                throw new Exception("沒有結帳項目");
            }

           

                try
                {

                    foreach (CartItemViewModel item in orderItemList)
                    {
                    /////先確認order的日期時間是否已經被占用
                    /////需要用到productId, orderId, date, timeId
                    //int productId = item.ProductId;
                    //var existedScheduleIds = (from o in orders
                    //                     join time in orderTimeSchedules
                    //                     on o.OrderId equals time.OrderId
                    //                     where o.ProductId == productId && time.ServiceDate.ToString() == item.ServiceDate
                    //                     select time.Schedule).ToList().Distinct();
                    


                        ///step1: Order表格開始

                        //處理保母ID
                        int sitterId = (from p in products
                                        where p.ProductId == item.ProductId
                                        select p.SitterId).SingleOrDefault();
                        var sitterName = (from sit in sitters
                                          where sit.MemberId == sitterId
                                          select sit.SitterName).SingleOrDefault();

                        //處理時間
                        var serviceDate = item.ServiceDate;
                        string[] serviceTime = item.ServiceTime.Split('~');
                        var beginTime = serviceTime[0].TrimEnd();
                        var endTime = serviceTime[1].TrimStart();
                        var serviceBeginTimeInit = $"{serviceDate} {beginTime}";
                        var serviceEndTimeInit = $"{serviceDate} {endTime}";
                        var beginTimeOutput = DateTime.Parse(serviceBeginTimeInit);
                        var endTimeOutput = DateTime.Parse(serviceEndTimeInit);
                        //處理產品介紹
                        var productIntro = (from p in products
                                            where p.ProductId == item.ProductId
                                            select p.Introduce).Single();
                        //處理地址
                        var cityName = item.CartCityName;
                        var distName = item.CartDistrictName;
                        var fullAddress = $"{cityName}{distName}{item.CartAddress}";

                        //處理orderNumber的亂數: S221001XXX
                        var orderNumber = string.Empty;

                        /*
                         * 洗澡: S開頭
                         * 散步: W開頭
                         * 照顧: C開頭
                        */
                        //step1: 處理開頭
                        var begin = string.Empty;
                        switch (item.Service)
                        {
                            case "到府照顧": begin = "C"; break;
                            case "到府洗澡": begin = "S"; break;
                            case "陪伴散步": begin = "W"; break;
                            default: break;
                        }
                        //step2: //暫時用服務的日期
                        var dateArr = serviceDate.Split('-'); // {2022,10,11}
                        var year = dateArr[0].Remove(0, 2); //2022 => 22
                        var date = dateArr[1] + dateArr[2]; //1011
                        var fulldate = year + date; //221011
                                                    //step3: 隨機亂數
                        var random = new Random().Next(1, 100);
                        var strNumber = random.ToString().PadLeft(3, '0');
                        orderNumber = begin + fulldate + strNumber;


                        //InvoiceId i.e. PW00000001
                        var invoiceTitle = "PW";
                        var invoiceNumOne = new Random().Next(0, 1000).ToString().PadLeft(4, '0');
                        var invoiceNumTwo = new Random().Next(0, 1000).ToString().PadLeft(4, '0');
                        var invoiceNumStr = invoiceNumOne + invoiceNumTwo;
                        var invoiceId = invoiceTitle + invoiceNumStr;
                        //檢查資料庫是否已經存在此InvoiceId
                        var isExisted = orders.Any(x => x.InvoiceId == invoiceId);
                        //如果有，抓取資料庫length直接對num做處理
                        while (isExisted)
                        {
                            invoiceNumOne = new Random().Next(0, 1000).ToString().PadLeft(4, '0');
                            invoiceNumTwo = new Random().Next(0, 1000).ToString().PadLeft(4, '0');
                            invoiceNumStr = invoiceNumOne + invoiceNumTwo;
                            invoiceId = invoiceTitle + invoiceNumStr;

                            isExisted = orders.Any(x => x.InvoiceId == invoiceId);
                        }


                        var order = new Order()
                        {
                            OrderNumber = orderNumber,
                            ProductId = item.ProductId,
                            SitterId = sitterId,
                            CustomerId = userId,
                            CreateTime = DateTime.UtcNow.AddHours(8),
                            OrderStatus = (int)OrderStatus.Success,
                            Amount = item.FinalCartPrice,
                            Discount = item.Discount, //待處理
                            InvoiceType = invoiceType,
                            InvoiceId = invoiceId,//待處理
                            UnoformNumber = invoiceCompanyTaxID,
                            CompanyName = invoiceCompanyTitle,
                            BookingName = bookName,
                            BookingEmail = bookEmail,
                            BookingPhone = bookPhone,
                            Address = fullAddress,
                            Name = item.ContactPersonName,
                            Phone = item.ContactPersonTel,
                            BeginTime = beginTimeOutput,
                            EndTime = endTimeOutput,
                            SitterName = sitterName,
                            ProductName = item.Service,
                            ProductIntro = productIntro,
                            ProductImageUrl = item.Photo,


                        };

                        _orderRepos.Add(order); //儲存到資料庫
                        orderIdsProduced.Add(order.OrderId); //傳出的orderID，未來要讓已完成訂單頁面知道抓取那些user的那些orderId

                        ///Order表格結束


                        ///step2: OrderSchedule開始
                        int beginScheduleId = timeSchedules.Where(x => x.TimeDesrcipt.Contains(beginTime)).Select(x => x.ScheduleId).SingleOrDefault();
                        int endScheduleId = timeSchedules.Where(x => x.TimeDesrcipt.Contains(endTime)).Select(x => x.ScheduleId).SingleOrDefault();
                        List<int> allScheduleIds = timeSchedules.Where(x => x.ScheduleId >= beginScheduleId && x.ScheduleId <= endScheduleId).Select(x => x.ScheduleId).ToList();
                        var serviceCount = allScheduleIds.Count;

                        foreach (int scheduleId in allScheduleIds)
                        {
                            var orderSchedule = new OrderSchedule()
                            {
                                OrderId = order.OrderId,
                                ServiceDate = DateTime.Parse(serviceDate),
                                Schedule = scheduleId

                            };

                            _orderScheduleRepos.Add(orderSchedule);

                        }
                        ///OrderSchedule結束


                        ///step3: OrderPetDetail開始
                        List<ProductServicePetType> productPetUnitPriceCombos = productServicePetTypes.Where(x => x.ProductId == item.ProductId).ToList();
                        List<PetInfoViewModel> petList = item.PetFullInfoList;

                        foreach (PetInfoViewModel pet in petList)
                        {
                            string petDescription;
                            var petDescriptJSON = pet.Description;
                            if (petDescriptJSON is null)
                            {
                                petDescription = string.Empty;
                            }

                            else
                            {
                                var desList = JsonConvert.DeserializeObject<string[]>(petDescriptJSON);
                                petDescription = string.Join(',', desList);
                            }



                            OrderPetDetail orderPetDetail = new OrderPetDetail()
                            {
                                PetName = pet.PetName,
                                PetType = pet.PetTypeId,
                                ShapeType = pet.ShapeTypeId,
                                PetSex = pet.PetSex == 1 ? true : false,
                                BirthYear = (int)pet.BirthYear,
                                BirthMonth = (int?)pet.BirthMonth,
                                Ligation = pet.Ligation,
                                Vaccine = pet.Vaccine,
                                OrderId = order.OrderId,
                                ServiceCount = serviceCount,
                                PetDiscription = petDescription,
                                PetIntro = pet.Remark,



                            };

                            _orderPetDetailRepos.Add(orderPetDetail);
                        }



                        ///OrderPetDetail結束



                    }

                }
                catch (Exception err)
                {
                    var msg = err.ToString();
                    Console.WriteLine($"發生錯誤，訊息：{msg}");
                }

            return orderIdsProduced;
        }
        #endregion

        #region 組綠界API要的資料
        public SendECPayDTO BuildECPayModel(List<int> savedOrderIds, List<int> savedCartIds)
        {
            ///需要查詢的資料表
            var orders = _orderRepos.GetAllReadOnly();
            var orderSchedules = _orderScheduleRepos.GetAllReadOnly();
            var orderPetDetails = _orderPetDetailRepos.GetAllReadOnly();
            var schedules = _timeScheduleRepos.GetAllReadOnly();

            //抓出品項資訊begin
            List<string> products = new List<string>();

            foreach (var orderId in savedOrderIds)
            {
                ///主要訂單資訊找出來
                var order = orders.Where(x => x.OrderId == orderId).SingleOrDefault();
                ///以下畫面處理用到
                var sitterName = order.SitterName;
                var serviceType = order.ProductName;

                //var price = order.Amount;
                //singlePrice.Add(price);

                //處理日期
                var serviceDate = (from o in orders
                                   join os in orderSchedules
                                   on o.OrderId equals os.OrderId
                                   where os.OrderId == orderId
                                   select os.ServiceDate.ToString("yyyy-MM-dd")).FirstOrDefault(); //
                //處理時間
                var timeScheduleIds = orderSchedules.Where(x => x.OrderId == orderId).OrderBy(x => x.Schedule).Select(x => x.Schedule).ToList();
                var beginTimeId = timeScheduleIds.First();
                var endTimeId = timeScheduleIds.Last();
                var beginTimeStr = schedules.Where(s => s.ScheduleId == beginTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[0];
                var endTimeStr = schedules.Where(s => s.ScheduleId == endTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[1];
                var serviceTime = beginTimeStr + " ~ " + endTimeStr; 
                var finalProduct = $"【{serviceType}】{sitterName}: {serviceDate} {serviceTime}"; //

                products.Add(finalProduct);
                
            }
            var productItems = string.Join('#', products);


            
            var finalPrice = orders.Where(x => savedOrderIds.Contains(x.OrderId)).Sum(x => x.Amount);
            int finalPriceInt = (int)finalPrice;

            var httpHeader = _httpContext.Request.Scheme;
            var hostValue = _httpContext.Request.Host.Value;
            
            
            //抓出品項資訊end

            var customField1 = string.Join(",", savedOrderIds);
            var customField2 = string.Join(",", savedCartIds);

            SendECPayDTO sendECPayDTO = new SendECPayDTO()
            {
                ///step1: 從資料庫找出這些order，找出以下商品描述

                //1. 商品品項: i.e. 手機20元X2#隨身碟60元X1  
                ItemName = productItems,
                
                //2. 商品訂單編號 
                MerchantTradeNo = "PAWS" + new Random().Next(0, 99999).ToString(),
                
                //3. 總結帳價格
                TotalAmount = finalPriceInt,

                CustomField1 = customField1,
                CustomField2 = customField2,

                ///step2 綠界參數
                //1. Controller/Action接收回傳網址
                
                ReturnURL = $"{httpHeader}://{hostValue}/{_returnUrlConfig.Value}", //暫時無效
                OrderResultURL = $"{httpHeader}://{hostValue}/{_resultUrlConfig.Value}",//2. 跳回自己頁面的網址

                ///step3 檢查碼，將以上資訊都包裝起來加密

            };
            var stringCheckMacValue = string.Empty;
            var rawData = 
                $"HashKey={sendECPayDTO.HashKey}" +
                $"&ChoosePayment={sendECPayDTO.ChoosePayment}" +
                //$"&ClientBackURL={sendECPayDTO.ClientBackURL}" + 
                $"&CustomField1={sendECPayDTO.CustomField1}" +
                $"&CustomField2={sendECPayDTO.CustomField2}" +
                $"&EncryptType={sendECPayDTO.EncryptType}" +
                $"&IgnorePayment={sendECPayDTO.IgnorePayment}" +
                $"&ItemName={sendECPayDTO.ItemName}" +
                $"&MerchantID={sendECPayDTO.MerchantID}" +
                $"&MerchantTradeDate={sendECPayDTO.MerchantTradeDate}" +
                $"&MerchantTradeNo={sendECPayDTO.MerchantTradeNo}" +
                $"&OrderResultURL={sendECPayDTO.OrderResultURL}" +
                $"&PaymentType={sendECPayDTO.PaymentType}" +
                $"&ReturnURL={sendECPayDTO.ReturnURL}" +
                $"&TotalAmount={(int)sendECPayDTO.TotalAmount}" +
                $"&TradeDesc={sendECPayDTO.TradeDesc}" +
                $"&HashIV={sendECPayDTO.HashIV}";
            
           var urlEncode = HttpUtility.UrlEncode(rawData).ToLower();

            if (sendECPayDTO.EncryptType != 1)
            {
                throw new Exception();
                
            }
            var hasher = new SHA256Hasher();
            urlEncode = hasher.HashPasseword(urlEncode);

            stringCheckMacValue = urlEncode;

            sendECPayDTO.CheckMacValue = stringCheckMacValue;
            //把檢查碼加回sendPayDTO裡面

            return sendECPayDTO;

        }
        #endregion

        public SendNewebPayDTO BuildNewebPayModel(List<int> savedOrderIds, List<int> savedCartIds)
        {
            ///需要查詢的資料表
            var orders = _orderRepos.GetAllReadOnly();
            var orderSchedules = _orderScheduleRepos.GetAllReadOnly();
            var orderPetDetails = _orderPetDetailRepos.GetAllReadOnly();
            var schedules = _timeScheduleRepos.GetAllReadOnly();

            //抓出品項資訊begin
            List<string> products = new List<string>();

            foreach (var orderId in savedOrderIds)
            {
                ///主要訂單資訊找出來
                var order = orders.Where(x => x.OrderId == orderId).SingleOrDefault();
                ///以下畫面處理用到
                var sitterName = order.SitterName;
                var serviceType = order.ProductName;

                //var price = order.Amount;
                //singlePrice.Add(price);

                //處理日期
                var serviceDate = (from o in orders
                                   join os in orderSchedules
                                   on o.OrderId equals os.OrderId
                                   where os.OrderId == orderId
                                   select os.ServiceDate.ToString("yyyy-MM-dd")).FirstOrDefault(); //
                //處理時間
                var timeScheduleIds = orderSchedules.Where(x => x.OrderId == orderId).OrderBy(x => x.Schedule).Select(x => x.Schedule).ToList();
                var beginTimeId = timeScheduleIds.First();
                var endTimeId = timeScheduleIds.Last();
                var beginTimeStr = schedules.Where(s => s.ScheduleId == beginTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[0];
                var endTimeStr = schedules.Where(s => s.ScheduleId == endTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[1];
                var serviceTime = beginTimeStr + " ~ " + endTimeStr;
                var finalProduct = $"【{serviceType}】{sitterName}: {serviceDate} {serviceTime}"; //

                products.Add(finalProduct);

            }
            var productItems = string.Join('#', products);



            var finalPrice = orders.Where(x => savedOrderIds.Contains(x.OrderId)).Sum(x => x.Amount);
            int finalPriceInt = (int)finalPrice;

            var hostValue = _httpContext.Request.Host.Value;
            var httpHeader = _httpContext.Request.Scheme;
            
            //抓出品項資訊end

            var customField1 = string.Join(",", savedOrderIds);
            var customField2 = string.Join(",", savedCartIds);

            var aes = System.Security.Cryptography.Aes.Create();
            //Encoding.UTF8.GetBytes(HashIV);

            var info = new BuildNewebPayInfo()
            {
                MerchantOrderNo = "PAWS" + new Random().Next(0, 99999).ToString(),
                Amt = finalPriceInt,
                ItemDesc = "寵物保母服務",
                NotifyURL = $"{httpHeader}://localhost:443/{_returnUrlConfig.Value}",//接收訊息
                ClientBackURL = $"{httpHeader}://{hostValue}/{_NewebResultURL.Value}" //導回頁面
            };
            info.NotifyURL = HttpUtility.UrlEncode(info.NotifyURL).ToLower();
            info.ClientBackURL = HttpUtility.UrlEncode(info.ClientBackURL).ToLower();

            //var urlEncode = HttpUtility.UrlEncode(rawData).ToLower();
            string tradeData = $"Amt={info.Amt}"+
                               $"&ItemDesc={info.ItemDesc}"+
                               $"&MerchantID={info.MerchantID}" +
                               $"&MerchantOrderNo={info.MerchantOrderNo}"+
                               $"&NotifyURL={info.NotifyURL}" +
                               $"&RespondType={info.RespondType}"+
                               $"&ClientBackURL={info.ClientBackURL}"+
                               $"&TimeStamp={info.TimeStamp}"+
                               $"&Version={info.Version}"
                               ;
            
            //string tradeInfo =  CryptoUtil.EncryptAESHex(tradeData, info.HashKey, info.HashIV);
            var tradeInfo = ApplicationCore.Extensions.HasherExtensions.EncryptAESHex(tradeData, info.HashKey, info.HashIV);
            string rawString = $"HashKey={info.HashKey}&{tradeInfo}&HashIV={info.HashIV}";
            var hasher = new SHA256Hasher();
            string tradeSha = hasher.HashPasseword(rawString);//送出

            var sendDto = new SendNewebPayDTO()
            {
                MerchantID = info.MerchantID,
                Version = info.Version,
                TradeInfo = tradeInfo,
                TradeSha = tradeSha
            };

            return sendDto;

        }



        #region 產生付款商品明細
        public List<OrderDetailDTO> CreatePaymentResult(List<int> orderIds)
        {
            var orders = _orderRepos.GetAll();
            var orderSchedules = _orderScheduleRepos.GetAll();
            var orderPetDetails = _orderPetDetailRepos.GetAll();
            var schedules = _timeScheduleRepos.GetAll();

            var listOfOrders = new List<OrderDetailDTO>();

            try 
            { 
                if(orderIds.Count == 0)
                {
                    throw new Exception();
                }
                foreach (var orderId in orderIds)
                {

                    ///主要訂單資訊找出來
                    var order = orders.Where(x => x.OrderId == orderId).SingleOrDefault();


                    ///以下畫面處理用到

                    //處理日期
                    var serviceDate = (from o in orders
                                       join os in orderSchedules
                                       on o.OrderId equals os.OrderId
                                       where os.OrderId == orderId
                                       select os.ServiceDate.ToString("yyyy-MM-dd")).FirstOrDefault();
                    //處理時間
                    var timeScheduleIds = orderSchedules.Where(x => x.OrderId == orderId).OrderBy(x => x.Schedule).Select(x => x.Schedule).ToList();
                    var beginTimeId = timeScheduleIds.First();
                    var endTimeId = timeScheduleIds.Last();
                    var beginTimeStr = schedules.Where(s => s.ScheduleId == beginTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[0];
                    var endTimeStr = schedules.Where(s => s.ScheduleId == endTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[1];

                    var serviceTime = beginTimeStr + " ~ " + endTimeStr;

                    //處理寵物資訊
                    var listOfPets = orderPetDetails.Where(p => p.OrderId == orderId).Select(p => new PetTitleDTO()
                    {
                        PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)p.PetType),
                        ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)p.ShapeType),
                    }).ToList();



                    ///創建訂單
                    var singleOrder = new OrderDetailDTO()
                    {
                        OrderNumber = order.OrderNumber,
                        PhotoUrl = order.ProductImageUrl,
                        SitterName = order.SitterName,
                        ServiceType = order.ProductName,
                        ServiceDate = serviceDate,
                        ServiceTime = serviceTime,
                        ListOfPets = listOfPets,
                        CartPrice = order.Amount
                    };

                    listOfOrders.Add(singleOrder);

                }


            }
            catch(Exception err) 
            {
                Console.WriteLine(err.ToString());
            }

            return listOfOrders;
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using PawsDay.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Linq;
using PawsDay.Services.ShoppingCart;
using System.Text.Json;
using PawsDay.ViewModels.ShoppingCart.Carts;
using ApplicationCore.Entities;
using ApplicationCore.Common;
using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using System.ComponentModel.DataAnnotations;
using PawsDay.ViewModels.Product;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced.DTO;
using System.Diagnostics;
using SendGrid;
using PawsDay.ViewModels.ShoppingCart.Booking;
using PawsDay.Services.Product;
using Microsoft.AspNetCore.Authorization;
using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using PawsDay.Models.ShoppingCart.WebApi.BaseModel;
using Infrastructure.Data;
using isRock.LineBot;
using PawsDay.Interfaces.Account;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using Infrastructure.Model;
using PawsDay.Services.MemberCenter;
using PawsDay.Services.SendGridServices;
using System.Text.Json.Serialization;

namespace PawsDay.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly CartServices _cartServices;
        private readonly BookingServices _bookingServices;
        private readonly OrderPlacedServices _orderPlacedServices;
        private readonly MemberCenterOrderServices _memberCenterOrderServices;
        private readonly SendGridService _sendGridService;

        private readonly ShoppingCartRepository _shoppingCartRepository;

        private readonly IRepository<PetInfomation> _petInformation;
        private readonly IAccountManager _accountManager;

        private readonly HttpContext _httpContext;
        public ShoppingCartController(CartServices cartServices, BookingServices bookingServices, OrderPlacedServices orderPlacedServices, IRepository<PetInfomation> petInformation, PawsDayContext pawsDayContext, ShoppingCartRepository shoppingCartRepository, IAccountManager accountManager, IHttpContextAccessor httpContextAccessor, MemberCenterOrderServices memberCenterOrderServices, SendGridService sendGridService)
        {
            _cartServices = cartServices;
            _bookingServices = bookingServices;
            _orderPlacedServices = orderPlacedServices;
            _petInformation = petInformation;
            _shoppingCartRepository = shoppingCartRepository;
            _accountManager = accountManager;
            _httpContext = httpContextAccessor.HttpContext;
            _memberCenterOrderServices = memberCenterOrderServices;
            _sendGridService = sendGridService;
        }

        #region 訪客Cookie設定
        [Authorize(Roles = AuthorizationConstants.NormalUser)]
        public IActionResult CookieRedirectPage()
        {

            var userId = _accountManager.GetLoginMemberId();
            string cookie = _httpContext.Request.Cookies["pawsdayCarts"];
            List<SaveCookieCartDTO> cookieSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SaveCookieCartDTO>>(cookie);

            //將cookie資料存到資料庫
            _shoppingCartRepository.SavingCookieToCartDB(userId, cookieSource);

            _httpContext.Response.Cookies.Delete("pawsdayCarts");

            return RedirectToAction("Cart");
            
        }

        public IActionResult VisitorCart()
        {

            return View();
        }
        #endregion


        public IActionResult Cart()
        {
            //var sw = new Stopwatch();
            //sw.Start();

            var userId = _accountManager.GetLoginMemberId();
            string JScardId = String.Empty;
            string JSShape = String.Empty;

            ListCartItemViewModel model = new ListCartItemViewModel();
            List<int> cartIdList = new List<int>();
            List<List<ChoosePetTypeDto>> shapeJSList = new List<List<ChoosePetTypeDto>>();

            if (userId == 0) //如果未登入
            {

                
                string cookie = _httpContext.Request.Cookies["pawsdayCarts"];
                List<CookieCartDTO> cookieSource = new List<CookieCartDTO>();

                if (cookie is null)
                {
                    model.validCartItemList = new List<CartItemViewModel>();
                    model.expiredCartItemList = new List<CartItemViewModel>();

                }
                else
                {
                    cookieSource = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CookieCartDTO>>(cookie);
                    model = _cartServices.CreateVisitorListOfCartItem(cookieSource);
                    foreach (var item in model.validCartItemList)
                    {
                        cartIdList.Add(item.CartId);
                        shapeJSList.Add(item.SelectedTypeOptions.ServiceShapeTypes);
                    }

                }


                ViewData["MemberId"] = userId; //給Recommend使用


                
                ViewBag.userId = userId;

                JScardId = JsonSerializer.Serialize(cartIdList);
                JSShape = JsonSerializer.Serialize(shapeJSList);
                
                ViewData["JScardId"] = JScardId;
                ViewData["JSShapeList"] = JSShape;

                return View("VisitorCart", model);

            }


            model = _cartServices.EstablishCartView(userId);

            ViewData["MemberId"] = userId; //給Recommend使用

            
            foreach (var item in model.validCartItemList)
            {
                cartIdList.Add(item.CartId);
                shapeJSList.Add(item.SelectedTypeOptions.ServiceShapeTypes);
            }
            ViewBag.userId = userId;

            JScardId = JsonSerializer.Serialize(cartIdList);
            JSShape = JsonSerializer.Serialize(shapeJSList);
            
            ViewData["JScardId"] = JScardId; 
            ViewData["JSShapeList"] = JSShape;

            //Console.WriteLine(sw.Elapsed);
            //sw.Stop();

            return View(model);
        }


        [HttpPost]
        //從商品頁直接到結帳頁面
        public IActionResult ProductToBooking(ProductToCartDto selected)
        {
            var userId = selected.MemberId;

            var member = _cartServices.GetMemberInfo(userId);

            var petInfos = _petInformation.GetAll();
            //var http = HttpContext.Request.Host;
            //Console.WriteLine(http);


            if (!ModelState.IsValid)
            {
                throw new Exception();
            }


            var cartItem = _cartServices.FromProductCreateCartItem(selected);

            List<CartItemViewModel> cartList = new List<CartItemViewModel>();
            cartList.Add(cartItem);


            List<PetInfomation> memberPetList = petInfos.Where(x => x.MemberId == userId).ToList();



            BookingInformationViewModel bookVM = new BookingInformationViewModel()
            {
                CartItemList = cartList,
                MemberPetsList= memberPetList,
                MemberName = member.MemberName,
                MemberAddress = member.MemberAddress,
                MemberTel = member.MemberTel,
                MemberEmail = member.MemberEmail,
                
            };

            var prices = new List<int>();
            int price = (int)(decimal.Parse(selected.SelectedPrice));
            prices.Add(price);

            ViewData["Prices"] = prices;
            ViewData["TotalPrice"] = (int)(decimal.Parse(selected.SelectedPrice));

            ViewData["BookVM"] = bookVM;


            var listDiscounts = cartList.Select(x => x.Discount).ToList();
           
            ViewData["Discounts"] = listDiscounts;



            return View("Booking");
        }

        

        [HttpPost]
        [Authorize(Roles = AuthorizationConstants.NormalUser)]
        public IActionResult Booking(ListCartItemViewModel source)
        {
            var userId = source.UserId;  //組VM用 B         


            var selectedCartIds = JsonSerializer.Deserialize<List<int>>(source.IndexOfSelectedItem); //組欄位用到 C

            var cartIds = source.validCartItemList.Select(x => x.CartId).ToList();

            List<decimal> listOfCorrectPrice = new List<decimal>();
            
            List<decimal> listOfCorrectDiscount = new List<decimal>();
            foreach(var id in selectedCartIds)
            {
                var price = source.validCartItemList.Where(x => x.CartId == id).SingleOrDefault().FinalCartPrice;

                listOfCorrectPrice.Add(price);
                var discount = source.validCartItemList.Where(x => x.CartId == id).SingleOrDefault().Discount;
                listOfCorrectDiscount.Add(discount);

            }


            int finalTotalPrice = Int32.Parse(source.FinalTotalPrice); //畫面用到 A
            ViewData["TotalPrice"] = finalTotalPrice;

            BookingInformationViewModel bookVM = _bookingServices.CreateBookingInformation(userId, selectedCartIds, listOfCorrectPrice, listOfCorrectDiscount); //畫面用到 A
            ViewData["BookVM"] = bookVM;

            OrderPlacedViewModel exportVM = _orderPlacedServices.CreateEmptyModel();

            return View(exportVM);
        }



        #region 結帳送出給金流
        [HttpPost]
        public IActionResult FromBookingToECPay(OrderPlacedViewModel data)
        {
            var user = _accountManager.GetLoginMemberId();

            // 存進資料庫之後，得到orderIds "List<int> savedOrderIds"
            List<int> savedOrderIds = _bookingServices.SavingBookingInformation(data, user);

            List<int> savedCartIds = new List<int>();
            foreach (var item in data.ListOfPayItems)
            {
                savedCartIds.Add(item.CartId);
            }

            

            //將orderIds放入組成綠界DTO的method
            SendECPayDTO ECPayVM = _bookingServices.BuildECPayModel(savedOrderIds,savedCartIds);


            return View(ECPayVM); //丟進去頁面自動跳轉綠界
        }

        [HttpPost]
        public IActionResult FromBookingToNewebPay(OrderPlacedViewModel data)
        {
            var user = _accountManager.GetLoginMemberId();

            // 存進資料庫之後，得到orderIds "List<int> savedOrderIds"
            List<int> savedOrderIds = _bookingServices.SavingBookingInformation(data, user);

            List<int> savedCartIds = new List<int>();
            foreach (var item in data.ListOfPayItems)
            {
                savedCartIds.Add(item.CartId);
            }

            List<string> strCartIds = savedCartIds.Select(x => x.ToString()).ToList();
            if (strCartIds.Count != 0)
            {
                _shoppingCartRepository.DeleteCompleteCart(strCartIds);
            }


            //將orderIds放入組成藍新DTO的method

            SendNewebPayDTO NewebPayVM = _bookingServices.BuildNewebPayModel(savedOrderIds, savedCartIds);
            //用Cookie帶資料
            //_httpContext.Request.Cookies

            _httpContext.Response.Cookies.Append("Neweb-OrderIds", Newtonsoft.Json.JsonConvert.SerializeObject(savedOrderIds));
            _httpContext.Response.Cookies.Append("Neweb-CartIds", Newtonsoft.Json.JsonConvert.SerializeObject(savedCartIds));



            return View(NewebPayVM);
        }

        [HttpPost]
        public IActionResult FromBookingToCoinPay(OrderPlacedViewModel data)
        {
            var user = _accountManager.GetLoginMemberId();
            List<int> savedOrderIds = _bookingServices.SavingBookingInformation(data, user);
            List<int> savedCartIds = new List<int>();
            foreach (var item in data.ListOfPayItems)
            {
                savedCartIds.Add(item.CartId);
            }

            ViewData["savedOrderIds"] = savedOrderIds;
            ViewData["savedCartIds"] = savedCartIds;


            int totalAmount = 0;
            foreach(var item in data.ListOfPayItems)
            {
                totalAmount += item.FinalCartPrice;
            }
            ViewData["FinalPrice"] = totalAmount;


            return View();
        }
        #endregion



        [HttpPost]
        //接收綠界回傳資料，並回覆OK
        public IActionResult ReturnECUrl(IFormCollection request)
        {
            Console.WriteLine(request);
            //可以再寫判斷回傳檢查碼的方法

            return Ok("1|OK");
        }

        #region 訂單完成明細
        [HttpPost]
        //綠界付款完成，轉跳頁面過來
        public IActionResult OrderPlaced(ReturnECPayDTO returnECData)
        {
            var userId = _accountManager.GetLoginMemberId();
            var strOrderIds = returnECData.CustomField1.Split(',');


            if (returnECData.RtnCode != 1)
            {
                List<string> orderNumbers = _orderPlacedServices.GetOrderNumbers(strOrderIds);

                _orderPlacedServices.DeleteFailedOrders(strOrderIds);

                ViewData["FailOrders"] = orderNumbers;

                return View("OrderFailure");
            }

            


            //cartIds的部分，要回去delete Cart關聯資料表
            var cartIds = returnECData.CustomField2.Split(',').ToList();
            var merchantTradeNo = returnECData.MerchantTradeNo;
            

            if (cartIds.Count != 0)
            {
                _shoppingCartRepository.DeleteCompleteCart(cartIds);
            }


            


            //orderIds的部分
            List<int> orderIds = new List<int>();
            
            foreach(var orderId in strOrderIds)
            {
                var intOrderId = int.Parse(orderId);
                orderIds.Add(intOrderId);


                //_orderPlacedServices.UpdateOrderSuccessStatus(intOrderId);


                //發出郵件通知
                var memberEmail = _memberCenterOrderServices.GetEmailOrderDTO(intOrderId, (int)UserType.Member);
                _sendGridService.CreateOrderEmailToMember(memberEmail).Wait();
                var sitterEmail = _memberCenterOrderServices.GetEmailOrderDTO(intOrderId, (int)UserType.Sitter);
                _sendGridService.CreateOrderEmailToSitter(sitterEmail).Wait();


            }

            List<OrderDetailDTO> orders = _bookingServices.CreatePaymentResult(orderIds);


            

            return View(orders);
        }

        [HttpGet]
        //Neweb付款完成，轉跳頁面過來
        public IActionResult NewebOrderPlaced()
        {
            var userId = _accountManager.GetLoginMemberId();

            string cookieOrderIds = _httpContext.Request.Cookies["Neweb-OrderIds"];
            string cookieCartIds = _httpContext.Request.Cookies["Neweb-CartIds"];

            List<int> intOrderIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(cookieOrderIds);
            List<int> intCartIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(cookieCartIds);
            var strCartIds = intCartIds.Select(x => x.ToString()).ToList();

            if (strCartIds.Count != 0)
            {
                _shoppingCartRepository.DeleteCompleteCart(strCartIds);
            }

            //orderIds的部分
            

            foreach (var orderId in intOrderIds)
            {


                //發出郵件通知
                var memberEmail = _memberCenterOrderServices.GetEmailOrderDTO(orderId, (int)UserType.Member);
                _sendGridService.CreateOrderEmailToMember(memberEmail).Wait();
                var sitterEmail = _memberCenterOrderServices.GetEmailOrderDTO(orderId, (int)UserType.Sitter);
                _sendGridService.CreateOrderEmailToSitter(sitterEmail).Wait();


            }

            List<OrderDetailDTO> orders = _bookingServices.CreatePaymentResult(intOrderIds);

            _httpContext.Response.Cookies.Delete("Neweb-OrderIds");
            _httpContext.Response.Cookies.Delete("Neweb-CartIds");

            return View("OrderPlaced",orders);
        }

        [HttpPost]
        //blocTo付款完成，轉跳頁面過來
        public IActionResult CoinOrderPlaced(ReturnBlocToDTO returnDto)
        {
            var userId = _accountManager.GetLoginMemberId();


            var intOrderIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(returnDto.OrderIds);
            var cartIds = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(returnDto.CartIds);
            var strCartIds = cartIds.Select(x => x.ToString()).ToList();


            if (strCartIds.Count != 0)
            {
                _shoppingCartRepository.DeleteCompleteCart(strCartIds);
            }
            
            

            foreach (var orderId in intOrderIds)
            {

                //發出郵件通知
                var memberEmail = _memberCenterOrderServices.GetEmailOrderDTO(orderId, (int)UserType.Member);
                _sendGridService.CreateOrderEmailToMember(memberEmail).Wait();
                var sitterEmail = _memberCenterOrderServices.GetEmailOrderDTO(orderId, (int)UserType.Sitter);
                _sendGridService.CreateOrderEmailToSitter(sitterEmail).Wait();


            }
            List<OrderDetailDTO> orders = _bookingServices.CreatePaymentResult(intOrderIds);
            foreach(var o in orders)
            {
                o.TxId = returnDto.TxId;
            }


            return View("OrderPlaced",orders);
        }
        #endregion

        [HttpPost]//綠界付款失敗，轉跳頁面過來
        public IActionResult OrderFailure()
        {
            return View();
        }


    }

}

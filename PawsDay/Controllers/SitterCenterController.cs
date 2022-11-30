using ApplicationCore.Common;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Models.SitterCenter;
using PawsDay.Services.MemberCenter;
using PawsDay.Services.SendGridServices;
using PawsDay.Services.SitterCenter;
using PawsDay.ViewModels.SitterCenter;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Controllers
{
    public class SitterCenterController : Controller
    {
        private readonly SitterCenterServices _services;
        private readonly SitterCenterBasicServices _basicservices;
        private readonly SitterCenterOrderServices _orderservices;
        private readonly SendGridService _sendGridService;
        public SitterCenterController(SitterCenterServices services, SendGridService sendGridService, SitterCenterBasicServices sitterCenterBasicServices, SitterCenterOrderServices sitterCenterOrderServices)
        {
            _services = services;
            _sendGridService = sendGridService;
            _basicservices = sitterCenterBasicServices;
            _orderservices = sitterCenterOrderServices;
        }

        #region 基本資料
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult Basic()
        {
            var response = _basicservices.GetBasic();
            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var viewmodel = new SitterBasicViewModel
            {
                MemberID = response.Data.MemberID,
                SitterName = response.Data.SitterName,
                SitterDescription = response.Data.SitterDescription,
            };
            return View(viewmodel);
        }


        [HttpPost]
        public async Task<IActionResult> Basic(SitterBasicViewModel input)
        {
            var request = new SitterBasicDto { MemberID = input.MemberID, SitterName = input.SitterName, SitterDescription = input.SitterDescription };
            var response = await (_basicservices.CreateBasic(request));
            if (response.IsSuccess == true)
            {
                input.IsValid = true;
                input.Message = "儲存成功";
                return RedirectToAction("Basic", input);
            }
            else
            {
                input.IsValid = false;
                input.Message = response.Message;
                return View("Basic", input);
            }

        }

        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult Account()
        {
            var response = _basicservices.GetAccount();
            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var viewmodel = new SitterAccountViewModel
            {
                MemberID = response.Data.MemberID,
                Account = response.Data.Account,
                Bank = response.Data.Bank,
                BankList = GetBankList()
            };
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Account(SitterAccountViewModel input)
        {
            var request = new SitterAccountDto
            {
                MemberID = input.MemberID,
                Bank = input.Bank,
                Account = input.Account,
            };

            var response = await (_basicservices.CreateAccout(request));
            if (response.IsSuccess == true)
            {
                input.IsValid = true;
                input.Message = "儲存成功";
                return RedirectToAction("Account", input);
            }
            else
            {
                input.IsValid = false;
                input.Message = response.Message;
                return View("Account", input);
            }
        }



        /// <summary>
        /// (WebApi)讀取特質圖
        /// </summary>

        public IActionResult Aptitude()
        {
            return View();
        }
        #endregion

        #region 商品列表
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ServiceIndex()
        {
            var response = _services.GetProductList(ProductStatus.OnSale);

            if (!response.IsSuccess)
            {
                return View("NotFound");
            }

            var viewmodel = response.Data.Select(p => new ProductViewModel
            {
                ProductID = p.ProductID,
                SitterName = p.SitterName,
                ServiceType = p.ServiceType,
                Introduce = p.Introduce.Substring(0, 30) + "...",
                ProductImage = p.MainImage,
                ServiceArea = p.ServiceArea.Select(area => new CountyandDistrictData 
                { District = area.DistrictName, County = area.CountyName })
            }).ToList();

            return View(viewmodel);
        }
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ServiceClose()
        {
            var response = _services.GetProductList(ProductStatus.OffSale);

            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var id = response.Data.Select(p => p.ProductID);
            var name = response.Data.Select(p => p.SitterName);
            var type = response.Data.Select(p => p.ServiceType);
            var intro = response.Data.Select(p => p.Introduce);
            var img = response.Data.Select(p => p.MainImage);
            var area = response.Data.Select(p => p.ServiceArea.Select(area => new CountyandDistrictData { District = area.DistrictName, County = area.CountyName }));

            var viewmodel = response.Data.Select(p => new ProductViewModel
            {
                ProductID = p.ProductID,
                SitterName = p.SitterName,
                ServiceType = p.ServiceType,
                Introduce = p.Introduce.Substring(0, 30) + "...",
                ProductImage = p.MainImage,
                ServiceArea = p.ServiceArea.Select(area => new CountyandDistrictData { District = area.DistrictName, County = area.CountyName })
            }).ToList();

            return View(viewmodel);
        }
        #endregion

        #region 促銷
        /// <summary>
        /// 新增、編輯、刪除、讀取促銷商品
        /// GetAllSales
        /// (WebApi)GetSales、 CreateSales、 UpdateSales、 DeleteSales
        /// </summary>
        /// 
        [Authorize(Roles = AuthorizationConstants.SitterUser)]

        public IActionResult ServiceSales()
        {
            var response = _services.GetProductDiscount();

            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var viewmodel = response.Data.Select(p => new ProductSalesViewModel
            {
                ProductID = p.ProductID,
                SitterName = p.SitterName,
                ServiceType = p.ServiceType,
                ProductImage = p.MainImage,
                ServiceArea = p.ServiceArea.Select(area => new CountyandDistrictData { District = area.District, County = area.County }),
                Discount = p.Discount,
                Quantity = p.Quantity,

            }).ToList();

            return View(viewmodel);
        }

        #endregion

        #region 廣告

        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ServiceAdvertise()
        {
            var response = _services.GetProductAd();
            if (!response.IsSuccess)
            {
                return View("NotFound");
            }
            
            var viewmodel = response.Data.Select(p => new ProductAdvertiseViewModel
            {
                ProductID = p.ProductID,
                SitterName = p.SitterName,
                ServiceType = p.ServiceType,
                ProductImage = p.MainImage,
                ServiceArea = p.ServiceArea.Select(area => new CountyandDistrictData
                {
                    County = area.CountyName,
                    District = area.DistrictName
                }),
                BeginDate = p.BeginDate,
                EndDate = p.EndDate
            }).ToList();

            return View(viewmodel);
        }
        #endregion

        #region 商品上架、編輯
        /// <summary>
        /// 商品規格
        /// Get: ServiceType、ProductIntro、SitterName、UpdateTime走Model存取
        /// Post: 全走WebApi
        /// 其餘資料打WebApi
        /// </summary>

        //建立
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult CreateServiceWithDetail()
        {
            var response = _services.GetSitterName();
            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var viewmodel = new ProductDetailViewModel
            {
                SitterName = response.Data,
                ServiceTypeList = CreateServiceList(),
                CountyList = _services.GetCounty(),
                DistrictList = _services.GetCountyandDistrict(),
            };
            return View("ServiceAdd", viewmodel);
        }

        //讀取
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult GetServiceWithDetail(int id)
        {
            var response = _services.GetProductDetail(id);
            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var viewmodel = new ProductDetailViewModel
            {
                ProductID = response.Data.ProductID,
                SitterName = response.Data.SitterName,
                ServiceType = response.Data.ServiceType,
                Introduce = response.Data.Introduce,
                UpdateTime = response.Data.UpdateTime.ToString() == null ? "" : response.Data.UpdateTime.ToString("yyyy-MM-dd HH:mm"),
                ServiceTypeList = CreateServiceList(),
                CountyList = _services.GetCounty(),
                DistrictList = _services.GetCountyandDistrict(),
            };

            return View("ServiceAdd", viewmodel);
        }

        #endregion

        #region 訂單(列表含sidebar、明細、取消)

        //訂單列表
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult OrderList()
        {
            var sw = new Stopwatch();
            sw.Start();

            var response = _orderservices.GetOrderList();

            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var viewmodel = response.Data.Select(o => new SitterOrderListViewModel
            {
                OrderID = o.OrderID,
                OrderNumber = o.OrderNumber,
                SitterID = o.SitterID,
                CustomerID = o.CustomerID,
                ServiceDay = o.ServiceDate,
                ServiceMonth = o.ServiceDate.ToString("MMMM", new System.Globalization.CultureInfo("en-us")).Substring(0, 3),
                ServiceDate = o.ServiceDate.Day,
                OrderStatus = ParseOrderStatus(o.PaymentStatus, o.ServiceDate),
                SitterName = o.SitterName,
                ProductName = o.ProductName,
                ProductImageUrl = o.ProductImageUrl,
                TotalPrice = o.TotalPrice.ToString("#,0")
            }).ToList();

            return View(viewmodel);
        }

        //訂單明細
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        [HttpGet]
        public IActionResult OrderDetail(string id)
        {
            var response = _orderservices.GetOrderDetail(id);

            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }

            var data = response.Data;
            var viewmodel = new SitterOrderDetailViewModel
            {
                OrderID = data.OrderID,
                OrderNumber = data.OrderNumber,
                SitterID = data.SitterID,
                CustomerID = data.CustomerID,
                CreateTime = data.CreateTime.AddHours(8),
                OrderStatus = ParseOrderStatus(data.OrderStatus, data.ServiceTime),
                TotalPrice = data.TotalPrice,
                InvoiceNumber = data.InvoiceNumber,
                ServiceTime = $"{data.BeginTime.ToString("HH:mm")}~{data.EndTime.ToString("HH:mm")}",
                BeginTime = data.BeginTime,
                ServiceTypeIntro = data.ServiceTypeIntro,
                ProductDetail = new OrderProductDetailData
                {
                    SitterName = data.SitterName,
                    ProductImageUrl = data.ProductImageUrl,
                    ProductIntro = data.ProductIntro,
                    ServiceType = data.ServiceType
                },
                CustomerDetail = new OrderCustomerDetailData
                {
                    BookingName = data.BookingName,
                    BookingPhone = data.BookingPhone,
                    BookingEmail = data.BookingEmail,
                    Addredss = data.Addredss,
                    Name = data.Name,
                    Phone = data.Phone,
                },
                PetDetails = data.PetDetails.Select(p => new PetInfoData {
                    PetName = p.PetName,
                    PetSex = p.PetSex,
                    PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)p.PetType),
                    ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)p.ShapeType),
                    Description = p.Description,
                    BirthYear = p.BirthYear,
                    BirthMonth = p.BirthMonth,
                    Ligation = p.Ligation,
                    Vaccine = p.Vaccine
                }),
                CancelData = new OrderCancelData
                {
                    OrderId = data.CancelDetail.OrderId,
                    CancelDate = data.CancelDetail.CancelDate,
                    CancelReason = data.CancelDetail.CancelReason
                },
                SideBar = data.SideBar
            };
            return View(viewmodel);
        }

        //取消訂單
        [HttpPost]
        public async Task<IActionResult> OrderDetail(SitterOrderDetailViewModel input)
        {
            var request = new SitterOrderCancelDto
            {
                OrderId = input.CancelData.OrderId,
                CancelDate = DateTime.UtcNow,
                CancelReason = input.CancelData.CancelReason
            };
            var response = await (_orderservices.CancelOrder(request));

            if (response.IsSuccess == true)
            {
                //成功寄送取消email
                var memberemail = _orderservices.GetEmailOrderDTO(input.CancelData.OrderId, (int)UserType.Member);
                _sendGridService.SitterCancelOrderEmailToMember(memberemail).Wait();
                var sitteremail = _orderservices.GetEmailOrderDTO(input.CancelData.OrderId, (int)UserType.Sitter);
                _sendGridService.SitterCancelOrderEmailToSitter(sitteremail).Wait();

                input.IsValid = true;
                input.Message = "儲存成功";
                return RedirectToAction("OrderDetail");
            }
            else
            {
                input.IsValid = false;
                input.Message = response.Message;
                return View("OrderDetail");
            }
        }

        #endregion

        #region 聊天室
        //聊天列表
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ChatRoomPawsDayList()
        {
            var chatroomlist = _orderservices.GetOfficialChatroom();
            return View(chatroomlist);
        }
        //聊天列表
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ChatRoomCustomerList()
        {
            var chatroomlist = _orderservices.GetCustomerChatroom();
            return View(chatroomlist);
        }

        //顧客聊天內容
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ChatRoomCustomer(int id)
        {
            var chatroom = _orderservices.GetCustomerContactDetail(id);
            if (chatroom is null)
            {
                return View("NotFound");
            }

            return View(chatroom);
        }


        //官方聊天內容
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ChatRoomPawsDay(string id)
        {
            var chatroom = _orderservices.GetOfficialContactDetail(id);
            if (chatroom is null)
            {
                return View("NotFound");
            }
            
            return View(chatroom);
        }


        #endregion

        #region 評論
        //訂單評論
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult OrderEvaluation(string id)
        {
            var response = _orderservices.GetEvaluation(id);
            if (response.IsSuccess == false)
            {
                return RedirectToAction("OrderDetail", "SitterCenter", new { id = id });
            }

            var viewmodel = new EvalutionViewModel
            {
                Orderid=response.Data.Orderid,
                OrderNumber=response.Data.OrderNumber,
                EvaluationScore=response.Data.EvaluationScore,
                EvaluationMessage=response.Data.EvaluationMessage,
                CreateTime=response.Data.CreateTime.AddHours(8),
                SideBar=response.Data.Sidebar
            };
            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> OrderEvaluation(EvalutionViewModel input)
        {
            var request = new SitterOrderEvaluationDto 
            { 
                Orderid= input.Orderid,
                OrderNumber=input.OrderNumber,
                EvaluationScore=input.EvaluationScore,
                EvaluationMessage=input.EvaluationMessage,
                CreateTime=DateTime.UtcNow,
            };
            var response = await _orderservices.CreateEvaluation(request);
            if (response.IsSuccess == true)
            {
                input.IsValid = true;
                input.Message = "儲存成功";
                return RedirectToAction("OrderEvaluation");
            }
            else
            {
                input.IsValid = false;
                input.Message = response.Message;
                return View("OrderEvaluation");
            }
        }

        //歷史評論
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult HistoryEvaluation()
        {
            var response = _orderservices.GetHistoryEvaluation();

            if (response.IsSuccess == false)
            {
                return View("NotFound");
            }
            var viewmodel = response.Data.Select(d=> new EvalutionViewModel
            {
                Orderid=d.Orderid,
                OrderNumber = d.OrderNumber,
                UserImageUrl = d.UserImageUrl,
                EvaluationScore = d.EvaluationScore,
                EvaluationMessage = d.EvaluationMessage,
                CreateTime = d.CreateTime.AddHours(8)
            }).ToList() ;
            return View(viewmodel);
        }
        #endregion

        #region 行事曆、報表
        //走webapi
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult Calendar()
        {
            return View();
        }
        //報表走api
        [Authorize(Roles = AuthorizationConstants.SitterUser)]
        public IActionResult ServiceChart()
        {
            return View();
        }
        #endregion

        #region 工具
        //服務類型選單
        public List<SelectListItem> CreateServiceList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text="請選擇服務類型",Disabled=true,Selected=true},
                new SelectListItem{ Text="到府照顧",Value="1"},
                new SelectListItem{ Text="到府洗澡",Value="2"},
                new SelectListItem{ Text="陪伴散步",Value="3"},
            };
        }
        //Banklist
        public List<SelectListItem> GetBankList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="請選擇",Disabled=true,Selected=true },
                new SelectListItem{Text="822中國信託",Value="822" },
                new SelectListItem{Text="808玉山銀行",Value="808" },
                new SelectListItem{Text="700中華郵政",Value="700" },
                new SelectListItem{Text="812台新銀行",Value="812" },
            };
        } 
        //OrderList服務狀態
        public string ParseOrderStatus(OrderStatus status, DateTime date)
        {
            if (status == OrderStatus.Success)
            {
                if (date < DateTime.Today) { return "服務結束"; }
                else { return "待服務"; }
            }
            else if (status == OrderStatus.Complete)
            {
                return "訂單完成";
            }
            else if (status == OrderStatus.Handle)
            {
                return "訂單處理中";
            }
            else
            {
                return "已取消";
            }
        }
        #endregion

    }
}

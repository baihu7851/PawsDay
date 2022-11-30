using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Services.MemberCenter;
using PawsDay.ViewModels.MemberCenter;
using System.Collections.Generic;
using System.Threading.Tasks;
using PawsDay.Services.SendGridServices;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;
using ApplicationCore.Constants;
using PawsDay.Interfaces.Account;
using ApplicationCore.Common;

namespace PawsDay.Controllers
{
    [Authorize(Roles = AuthorizationConstants.NormalUser)]
    public class MemberCenterController : Controller
    {
        private readonly PawsDayContext _context;
        private readonly HistoryEvaluationViewModelService _evaluationViewModelService;
        private readonly MemberCenterOrderServices _orderServices;
        private readonly MemberPetInfoService _petInfoService;
        private readonly CollectViewModelServices _collectViewModelServices;
        private readonly ChatroomViewModelService _chatroomViewModelService;
        private readonly PersonInfoServices _personInfoServices;
        private readonly SendGridService _sendGridService;
        private readonly MemberCenterCalenderService _memberCenterCalenderService;
        private readonly IAccountManager _accountManager;

        private int userId;

        public MemberCenterController(PawsDayContext context, HistoryEvaluationViewModelService evaluationViewModelService, MemberCenterOrderServices orderServices, MemberPetInfoService petInfoService, CollectViewModelServices collectViewModelServices, ChatroomViewModelService chatroomViewModelService, PersonInfoServices personInfoServices, SendGridService sendGridService, MemberCenterCalenderService memberCenterCalenderService, IAccountManager accountManager)
        {
            _context = context;
            _evaluationViewModelService = evaluationViewModelService;
            _orderServices = orderServices;
            _petInfoService = petInfoService;
            _collectViewModelServices = collectViewModelServices;
            _chatroomViewModelService = chatroomViewModelService;
            _personInfoServices = personInfoServices;
            _sendGridService = sendGridService;
            _memberCenterCalenderService = memberCenterCalenderService;
            _accountManager= accountManager;
            userId = _accountManager.GetLoginMemberId();
        }

        [HttpGet]
        public IActionResult PersonInformation()
        {
            
            ViewBag.Data = userId;
            var person = _personInfoServices.GETPersonInfo(userId);
            person.GenderList = GetSex();
            return View(person);
        }
        [HttpPost]
        public IActionResult PersonInformation(PersonInformationViewModel input)
        {
            ViewBag.Data = userId;
            bool IsSuccess = _personInfoServices.UpdatePersonInfo(input, userId);
            var person = _personInfoServices.GETPersonInfo(userId);
            person.GenderList = GetSex();
            return View(person);
        }

        [HttpGet]//後端傳資料到前端
        public IActionResult PetInformation()
        {
            ViewBag.Data = userId;

            ViewData["pets"] = _petInfoService.GetPetList(ViewBag.Data);
            var petList = GetVMList();

            return View(petList);
        }
        
        [HttpPost] //前端傳資料到後端
        [ValidateAntiForgeryToken]
        public IActionResult CreatePetInformation(MemberPetInfoViewModel memberPetInfoVM)
        {
            ViewBag.Data = userId;
            if (!ModelState.IsValid)
            {
                //渲染List-未完成
                var petList = GetVMList();
                return View("PetInformation", memberPetInfoVM);
            }

            var memberId = _accountManager.GetLoginMemberId();

            _petInfoService.CreatePetData(memberPetInfoVM , memberId);


             return Redirect("/MemberCenter/PetInformation");
        }

        #region 帳號安全
        [HttpGet]
        public IActionResult AccountSafety()
        {
            ViewBag.Data = userId;
            var viewMsg = new AccountSafetyViewModel { updateAccount=new UpdateAccount { IsUpdate=false} };
            return View(viewMsg);
        }
        [HttpPost]
        public IActionResult AccountSafety(AccountSafetyViewModel input)
        {
            ViewBag.Data = userId;
            var updateMsg = _personInfoServices.UpdateAccountSafety(userId, input);
            var viewMsg= new AccountSafetyViewModel{ updateAccount=updateMsg };
            return View(viewMsg);
        }
        #endregion

        #region 聊天室
        [HttpGet]
        public IActionResult ChatroomPawsday()
        {
            ViewBag.Data = userId;
            var chatroomPawsday = _chatroomViewModelService.GetChatroomPawsday(userId);
            return View(chatroomPawsday);
        }
        [HttpGet]
        public IActionResult ChatroomSister()
        {
            ViewBag.Data = userId;
            var chatroomSisters = _chatroomViewModelService.GetChatroomSister(userId);
            return View(chatroomSisters);
        }
        [HttpGet]
        public IActionResult ChatroomSisterDetail(int id)
        {
            ViewBag.Data = userId;
            var sisterDetail = _chatroomViewModelService.GetChatroomSisterDetail(userId, id);
            if (sisterDetail is null)
            {
                return RedirectToAction("Index","Home");
            }
            return View(sisterDetail);
        }
       
        #endregion

        #region 行事曆
        [HttpGet]
        public IActionResult Calendar()
        {
            return View();
        }
         
        #endregion

        #region 我的收藏
        public IActionResult Collect()
        {
            ViewBag.Data = userId;
            return View();
        }
        #endregion

        #region 訂單
        public IActionResult Order()
        {
            ViewBag.Data = userId;
            var orderlist = _orderServices.GetOrderList(userId);

            //_sendGridService.CancelOrderEmail1(x).Wait();
            return View(orderlist);

        }

        [HttpGet]
        public  IActionResult OrderDetail(string id)
        {            
            var orderId = _orderServices.GetOrderId(id);
            if (orderId == 0) 
            {
                return RedirectToAction("Index", "Home");
            }

            var orderDetail = _orderServices.GetOrderDetail(orderId);
            var ordersidebar = _orderServices.GetOrderPartial(orderId);
            orderDetail.memberCenterOrderSidebarViewModel = ordersidebar;
            orderDetail.orderCancelViewModel.CancelReasonList = GetCancelReasonList();

            var memberEmail = _orderServices.GetEmailOrderDTO(orderId,(int)UserType.Member);
             _sendGridService.CreateOrderEmailToMember(memberEmail).Wait();
            var sitterEmail = _orderServices.GetEmailOrderDTO(orderId, (int)UserType.Sitter);
            _sendGridService.CreateOrderEmailToSitter(sitterEmail).Wait();

            return View(orderDetail);
        }
        [HttpPost]
        public IActionResult OrderDetail(OrderDetailViewModel input)
        {
            var orderId = input.memberCenterOrderSidebarViewModel.OrderId;
            bool IsSuccess = _orderServices.CreateCancelOrder(orderId, input.orderCancelViewModel.CancelReason);
            if (IsSuccess)
            {
                var memberEmail = _orderServices.GetEmailOrderDTO(orderId, (int)UserType.Member);
                _sendGridService.MemberCancelOrderEmailToMember(memberEmail).Wait();
                var sitterEmail = _orderServices.GetEmailOrderDTO(orderId, (int)UserType.Sitter);
                _sendGridService.MemberCancelOrderEmailToSitter(sitterEmail).Wait();
            }
            var orderDetail = _orderServices.GetOrderDetail(orderId);
            var ordersidebar = _orderServices.GetOrderPartial(orderId);
            orderDetail.memberCenterOrderSidebarViewModel = ordersidebar;

            return View(orderDetail);
        }

        [HttpGet]
        public IActionResult OrderEvaluation(string id)
        {
            var orderId = _orderServices.GetOrderId(id);
            if (orderId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var orderEvaluation = _orderServices.GetOrderEvaluation(orderId);
            if (orderEvaluation is null)
            {
                return RedirectToAction("OrderDetail", "MemberCenter", new { id = id });
            }
            var ordersidebar = _orderServices.GetOrderPartial(orderId);
            orderEvaluation.memberCenterOrderSidebarViewModel = ordersidebar;
            return View(orderEvaluation);
        }
        [HttpPost]
        public IActionResult OrderEvaluation(OrderEvaluationViewModel input)
        {
            var orderId = input.OrderId;
            bool IsSuccess = _orderServices.CreateOrderEvaluation(input, userId, orderId);
            var orderEvaluation = _orderServices.GetOrderEvaluation(orderId);
            var ordersidebar = _orderServices.GetOrderPartial(orderId);
            orderEvaluation.memberCenterOrderSidebarViewModel = ordersidebar;
            return View(orderEvaluation);
        }
        [HttpGet]
        public IActionResult OrderContact(string id)
        {
            var orderId = _orderServices.GetOrderId(id);
            if (orderId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var orderContact = _chatroomViewModelService.GetOrderContactDetail(orderId);
            var ordersidebar = _orderServices.GetOrderPartial(orderId);
            orderContact.memberCenterOrderSidebarViewModel = ordersidebar;
            return View(orderContact);
        }       

        #endregion

        #region 歷史評論
        public IActionResult HistoryEvaluation()
        {
            ViewBag.Data = userId;
            var evaluations = _evaluationViewModelService.GetHistoryEvaluations(userId);
            return View(evaluations);
        }
        #endregion

        #region Select
        private List<SelectListItem> GetSex()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="-請選擇-",Disabled=true,Selected=true },
                new SelectListItem{Text="男性",Value="true" },
                new SelectListItem{Text="女性",Value="false" }
            };
        }
        private List<SelectListItem> GetCancelReasonList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="-選擇取消原因-",Disabled=true,Selected=true },
                new SelectListItem{Text="個人因素",Value="個人因素" },
                new SelectListItem{Text="聯絡不上保姆",Value="聯絡不上保姆" },
                new SelectListItem{Text="行程無法銜接",Value="行程無法銜接" }
            };
        }
        private List<SelectListItem> GetPetType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="-請選擇毛孩類型-",Disabled=true,Selected=true},
                new SelectListItem{Text="貓咪",Value="1" },
                new SelectListItem{Text="狗狗",Value="0" }
            };
        }

        public List<SelectListItem> GetShapeType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text="-請選擇毛孩體型-",Disabled=true,Selected=true},
                new SelectListItem{ Text="迷你型(5kg以下)",Value="0"},
                new SelectListItem{ Text="小型(5~10kg以下)",Value="1"},
                new SelectListItem{ Text="中型(10~20kg以下)",Value="2"},
                new SelectListItem{ Text="大型(20~40kg以下)",Value="3"},
                new SelectListItem{ Text="超大型(20kg以上)",Value="4"},
            };

        }
        public List<SelectListItem> GetBool()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "是", Value = "true" },
                new SelectListItem { Text = "否", Value = "false" }
            };
            
        }
        public List<SelectListItem> GetMonth()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "-請選擇-", Disabled = true, Selected = true },
                new SelectListItem { Text = "1月", Value = "1" },
                new SelectListItem { Text = "2月", Value = "2" },
                new SelectListItem { Text = "3月", Value = "3" },
                new SelectListItem { Text = "4月", Value = "4" },
                new SelectListItem { Text = "5月", Value = "5" },
                new SelectListItem { Text = "6月", Value = "6" },
                new SelectListItem { Text = "7月", Value = "7" },
                new SelectListItem { Text = "8月", Value = "8" },
                new SelectListItem { Text = "9月", Value = "9" },
                new SelectListItem { Text = "10月", Value = "10" },
                new SelectListItem { Text = "11月", Value = "11" },
                new SelectListItem { Text = "12月", Value = "12" },
            };
        }
        private List<SelectListItem> GetPetSex()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="-請選擇-",Disabled=true,Selected=true },
                new SelectListItem{Text="男孩",Value="1" },
                new SelectListItem{Text="女孩",Value="0" }
            };
        }

        public MemberPetInfoViewModel GetVMList()
        {
            //渲染畫面
            var petList = new MemberPetInfoViewModel
            {
                PetGenderList = GetPetSex(),
                PetTypeList = GetPetType(),
                ShapeTypeList = GetShapeType(),
                BirthMonthList = GetMonth(),
                PetDispositionList = _petInfoService.GetPetDispositionList(),
                LigationList = GetBool(),
                VaccineList = GetBool()
            };
            
            return petList;
        }
        #endregion
    }
}
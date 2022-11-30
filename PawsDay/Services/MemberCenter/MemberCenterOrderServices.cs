using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PawsDay.Models.MemberCenter;
using PawsDay.Services.SendGridServices.DTO;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.SitterCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PawsDay.Services.MemberCenter
{
    public class MemberCenterOrderServices
    {
        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _sitter;
        private readonly IRepository<Order> _order;
        private readonly IRepository<OrderPetDetail> _orderPetDetail;
        private readonly IRepository<Evaluation> _evaluation;
        private readonly IRepository<OrderSchedule> _orderSchedule;
        private readonly IRepository<OrderCancel> _orderCancel;

        

        public MemberCenterOrderServices(IRepository<Member> member, IRepository<RegisterSitter> sitter, IRepository<Order> order, IRepository<OrderPetDetail> orderPetDetail, IRepository<Evaluation> evaluation, IRepository<OrderSchedule> orderSchedule, IRepository<OrderCancel> orderCancel)
        {
            _member = member;
            _sitter = sitter;
            _order = order;
            _orderPetDetail = orderPetDetail;
            _evaluation = evaluation;
            _orderSchedule = orderSchedule;
            _orderCancel = orderCancel;
        }

        public MemberCenterOrderSidebarViewModel GetOrderPartial(int orderId)
        {
            var orderDetail = GetOrder(orderId);

            var petList = _orderPetDetail.GetAllReadOnly().Where(x => x.OrderId == orderDetail.OrderId).Select(x => new OrderPetDTO { PetType=x.PetType,ShapeType=x.ShapeType}).ToList();

            var petGroupPetType= petList.GroupBy(x => x.PetType);

            var orderPetLListDTO = new List<OrderPetLListDTO>();

            foreach (var pettype in petGroupPetType) 
            {
                var petGroupShapeType = petList.Where(x => x.PetType== pettype.Key).GroupBy(x=>x.ShapeType);
                foreach (var petshape in petGroupShapeType)
                {
                    var petlist = petList.Where(x => x.PetType == pettype.Key && x.ShapeType == petshape.Key).Count();
                    orderPetLListDTO.Add(new OrderPetLListDTO {PetType= pettype.Key,ShapeType= petshape.Key,Count=petlist });
                }
            }

            var OrderCancelDTO = new OrderCancelDTO();
            decimal cancelPrice = 0;
            if (orderDetail.OrderStatus ==(int)OrderStatus.Cancel)
            {
                OrderCancelDTO = GetOrderCancelDTO(orderId);
                cancelPrice = orderDetail.Amount-orderDetail.Amount * OrderCancelDTO.Persent;
            }
            else
            {
                OrderCancelDTO = null;
            }

            var orderDetailPartial = new MemberCenterOrderSidebarViewModel
            { 
                OrderId=orderId,
                SitterId=orderDetail.SitterId,
                ProductId=orderDetail.ProductId,
                ProductImage=orderDetail.ProductImageUrl,
                SitterName=orderDetail.SitterName,
                ServiceName=orderDetail.ProductName,
                ServiceDate =orderDetail.BeginTime,
                TotolPrice=orderDetail.Amount,
                CancelPrice= Math.Round(cancelPrice),
                ReturnPrice= Math.Round(orderDetail.Amount-cancelPrice),
                OrderPetLListDTO = orderPetLListDTO,
                OrderStatus=orderDetail.OrderStatus,
                ServiceTime=$"{orderDetail.BeginTime.ToString("HH: mm")}-{orderDetail.EndTime.ToString("HH: mm")}"
            };

            return orderDetailPartial;
        }

        public IEnumerable<OrderViewModel> GetOrderList(int userId)
        {
            var orders = (from o in _order.GetAllReadOnly()
                            where o.CustomerId == userId
                            orderby o.OrderId descending
                            select new OrderViewModel
                            {
                                OrderId = o.OrderId,
                                ProductId = o.ProductId,
                                SitterId = o.SitterId,
                                SitterName=o.SitterName,
                                OrderStatus = o.OrderStatus,
                                ProductImage = o.ProductImageUrl,
                                ServiceName = o.ProductName,
                                OrderNumber = o.OrderNumber,
                                TotalPrice=o.Amount,
                                ServiceDate=o.BeginTime,
                            }).ToList();

            var orderList = orders.GroupBy(o => o.OrderId).ToList();
            foreach (var item in orderList)
            {
                yield return item.First();
            }
            
        }

        public OrderDetailViewModel GetOrderDetail(int orderId)
        {
            var orderDetail = GetOrder(orderId);
            if (orderDetail is null)
            { 
                return null;
            }
            var petList = _orderPetDetail.GetAllReadOnly().Where(x => x.OrderId == orderDetail.OrderId).Select(x => new OrderPetList
            {
                OrderPetId = x.OrderPetId,
                PetName =x.PetName,
                PetType= GetPetType(x.PetType),                
                ShapeType= GetPetShape(x.ShapeType),
                Gender=x.PetSex == true ? "男性" : "女性",
                Discription=x.PetDiscription,
                BirthYear = x.BirthYear,
                Ligation = x.Ligation==true?"是":"否",
                Vaccine = x.Vaccine == true ? "是" : "否",
                PetText=x.PetIntro
            }).ToList();

            var orderDetailViewModel = new OrderDetailViewModel
            { OrderNum = orderDetail.OrderNumber,
                MemberName = orderDetail.BookingName,
                MemberEmail = orderDetail.BookingEmail,
                MemberPhone = orderDetail.BookingPhone,
                ProductIntro =  orderDetail.ProductIntro,
                ServiceIntro= GetServiceType(orderDetail.ProductName),
                ServiceName = orderDetail.ProductName,
                Address = orderDetail.Address,
                ConnectionName = orderDetail.Name,
                ConnectionPhone = orderDetail.Phone,
                CreateTime = orderDetail.CreateTime,
                OrderStatus = orderDetail.OrderStatus,
                ServiceTime= orderDetail.BeginTime,
                OrderPetList = petList,
                orderCancelDTO= GetOrderCancelDTO(orderId),
                orderCancelViewModel= GetOrderCancelViewModel(orderId),
                InvoiceID=orderDetail.InvoiceId
            };

            return orderDetailViewModel;
        }
        private Order GetOrder(int orderId)
        {
            return _order.GetAllReadOnly().FirstOrDefault(x => x.OrderId == orderId);            
        }
        public int GetOrderId(string orderNum)
        {
            var order = _order.GetAllReadOnly().FirstOrDefault(x => x.OrderNumber == orderNum);
            if (order is null)
            {
                return 0;
            }
            else
            { 
                return order.OrderId;
            }
            

        }
        #region 寄送email

        public EmailOrderDTO GetEmailOrderDTO(int orderId,int userType)
        {    
            var order = GetOrder(orderId);
            var email = new EmailOrderDTO
            {               
                ContentDTO= GetEmailContentDTO(order,userType),
                SitterName =order.SitterName,
                MemberName= order.BookingName,
                ServiceName=order.ProductName,
                OrderNum=order.OrderNumber,
                ServiceDate=order.BeginTime.ToString("yyyy-MM-dd"),
                ServiceTime=$"{order.BeginTime.ToString("HH:mm")}-{order.EndTime.ToString("HH:mm")}",
            };
            if (order.OrderStatus == (int)OrderStatus.Cancel)
            {
                var ordercancel = GetOrderCancelDTO(orderId);
                email.EmailCancelOrderDTO = new EmailCancelOrderDTO 
                { 
                    CancelReason= ordercancel.CancelReason,
                    CreatrTime=ordercancel.CancelDate.ToString("yyyy-MM-dd HH:mm"),
                    CancelBackAmount= Math.Round(order.Amount * ordercancel.Persent) 
                };
            }
            return email;
        }
        private EmailContentDTO GetEmailContentDTO(Order order, int userType)
        {
            var ContentDTO = new EmailContentDTO();
            if (userType == (int)UserType.Member)
            {
                ContentDTO.UserName = order.BookingName;
                ContentDTO.UserEmail = order.BookingEmail;                
            }
            else
            {
                ContentDTO.UserName = order.SitterName;
                ContentDTO.UserEmail = _member.GetById(order.SitterId).Email;                
            }
            return ContentDTO;  
        }

        #endregion

        #region 取消訂單
        private OrderCancelDTO GetOrderCancelDTO(int orderId)
        {
            var dto = _orderCancel.GetAllReadOnly().FirstOrDefault(x => x.OrderId == orderId);
            if (dto != null)
            {
                var cancelDto = new OrderCancelDTO
                {
                    OrderId = orderId,
                    CancelDate = dto.CancelDate.AddHours(8),
                    CancelReason = dto.CancelReason,
                    Persent = dto.RefundPersent
                };
                return cancelDto;
            }
            else
            { 
                return null;
            }            
        }        
        private OrderCancelViewModel GetOrderCancelViewModel(int orderId)
        {
            var order = GetOrder(orderId);
            var cancelViewModel = new OrderCancelViewModel
            { 
                BackZeroDate=$"{ order.BeginTime.ToString("yyyy-MM-dd")}當日",
                BackHalfDate =$"{order.BeginTime.AddDays(-5).ToString("yyyy-MM-dd")}~{order.BeginTime.AddDays(-1).ToString("yyyy-MM-dd")}",
                BackAllDate =$"{order.BeginTime.AddDays(-6).ToString("yyyy-MM-dd")}前",
                BackPrice=Math.Round(order.Amount*GetBackPrice(order.BeginTime))
            };
            return cancelViewModel;
        }
        private decimal GetBackPrice(DateTime servicedate)
        {
            if (DateTime.UtcNow.Date == servicedate.Date)
            {
                return 0m;
            }
            else if ((servicedate.Date-DateTime.UtcNow.Date ).Days < 5)
            {
                return 0.5m;
            }
            else
            {
                return 1m;
            }                
        }

        public bool CreateCancelOrder(int orderId,string message)
        {
            bool IsSuccess;
            var order = GetOrder(orderId);
            var cancel = new OrderCancel 
            { 
                OrderId=orderId,
                CancelDate=DateTime.UtcNow,
                CancelReason=message,
                RefundPersent= GetBackPrice(order.BeginTime)
            };           
            
            try
            {
                order.OrderStatus = (int)OrderStatus.Cancel;
                _orderCancel.Add(cancel);
                _order.Update(order);
                IsSuccess = true;
                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }


        }
        #endregion

        #region 訂單評論
        public OrderEvaluationViewModel GetOrderEvaluation(int orderId)
        {
            var order = GetOrder(orderId);
            bool IsSuccess = CheckOrderEvaluation(order);
            if (!IsSuccess)
            { 
                return null;
            }
            var evaluation = _evaluation.GetAllReadOnly().FirstOrDefault(x => x.OrderId == orderId && x.UserType==1);
            if (evaluation is null)
            {
                return new OrderEvaluationViewModel() { OrderNum=order.OrderNumber, OrderId = orderId };
            }
            else
            {
                var orderEvaluation = new OrderEvaluationViewModel
                {
                    Evaluation=evaluation.EvaluationScore,
                    Message=evaluation.Message,
                    OrderNum = order.OrderNumber,
                    OrderId=orderId
                };
                return orderEvaluation;
            }
        }
        private bool CheckOrderEvaluation(Order order)
        {
            var serviceTime = _orderSchedule.GetAllReadOnly().Where(x => x.OrderId == order.OrderId).First().ServiceDate;
            if (serviceTime > DateTime.UtcNow || order.OrderStatus == (int)OrderStatus.Cancel)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }

        public bool CreateOrderEvaluation(OrderEvaluationViewModel input,int userId,int orderId)
        {
            
            var evaluationDetail = new Evaluation
            {
                OrderId = orderId,
                UserId = userId,
                UserType = 1,
                EvaluationScore = input.Evaluation,
                Message = input.Message,
                CreateTime = DateTime.UtcNow
            };
            bool IsSuccess;
            try
            {
                _evaluation.Add(evaluationDetail);
                IsSuccess = true;
                return IsSuccess;
            }
            catch
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }
        #endregion

        #region 服務說明文字
        private string GetServiceType(string type)
        {
            switch (type)
            {
                case "到府照顧":
                    return "＄200元起／30分鐘\r\n\r\n<br>👉 專業實名認證寵物保姆到府照顧寵物\r\n\r\n<br>👉 寵物保姆、餵食、環境清潔、陪伴玩耍、回報健康狀況、餵藥等客製服務\r\n\r\n<br>👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n\r\n<br>👉 全程與保姆維持連線，回報寵物狀況\r\n\r\n<br>👉 平台預約全程含青杉保險與品質保障\r\n\r\n<br>👉 鑰匙可以溝通警衛代收、信箱傳遞等方式";
                case "到府洗澡":
                    return "＄300元起／1小時起／1隻毛孩\r\n    \r\n    <br>👉 寵物免出門! 寵物美容師攜帶工具到府幫寵物做小美容\r\n    \r\n    <br>👉 小美容包含洗澡、按摩、剪指甲、清耳朵、擠肛門腺、修腳底毛、修屁股毛、含環境清理\r\n    \r\n    <br>👉 服務前美容師會先跟毛孩培養感情、餵零食\r\n    \r\n    <br>👉 若有特殊需求或是疾病毛孩，請先與美容師溝通\r\n    \r\n    <br>👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n    \r\n    <br>👉 全程與保姆維持連線，回報寵物狀況\r\n    \r\n    <br>👉 平台預約全程含青杉保險與品質保障";
                default:
                    return "＄100元起／30分鐘起\r\n\r\n<br>👉 無法掌控回家時間? 保姆可到府帶狗狗出門散步\r\n\r\n<br>👉 出門不能帶狗狗進餐廳? 保姆可約地點接狗狗散步\r\n\r\n<br>👉 每次接待少量的毛小孩，細心顧及每個毛小孩的需求\r\n\r\n<br>👉 全程與保姆維持連線，回報寵物狀況\r\n\r\n<br>👉 平台預約全程含青杉保險與品質保障\r\n\r\n<br>👉 鑰匙可以溝通警衛代收、信箱傳遞等方式";
            }
        }
        #endregion

        #region 寵物轉換
        public static string GetPetType(int type)
        {
            switch (type)
            {
                case 0:
                    return "狗狗";
                default:
                    return "貓咪";
            }
        }
        public static string GetPetShape(int type)
        { 
            switch (type)
            {
                case 0:
                    return "迷你型(5kg以下)";
                case 1:
                    return "小型(5~10kg以下)";
                case 2:
                    return "中型(10~20kg以下)";
                case 3:
                    return "大型(20~40kg以下)";
                default:
                    return "超大型(20kg以上)";
            }
        }
        #endregion

    }
}

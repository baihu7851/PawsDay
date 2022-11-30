using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.CodeAnalysis;
using PawsDay.Interfaces.Account;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Services.SendGridServices.DTO;
using PawsDay.ViewModels.SitterCenter;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Services.SitterCenter
{

    public  class SitterCenterOrderServices
    {

        private readonly IRepository<RegisterSitter> _registersitter;
        private readonly IRepository<ApplicationCore.Entities.Product> _product;
        private readonly IRepository<Order> _order;
        private readonly IRepository<OrderPetDetail> _orderpetdetail;
        private readonly IRepository<OrderCancel> _ordercancel;
        private readonly IRepository<OrderSchedule> _orderschedule;
        private readonly IRepository<Member> _member;
        private readonly IRepository<Evaluation> _evaluation;
        private readonly IRepository<UserContact> _usercontact;
        private readonly IRepository<OfficialContact> _officialcontact;
        private readonly IAccountManager _accountManager;

        int memberid;
        public SitterCenterOrderServices
            (IRepository<RegisterSitter> registersitter,
            IRepository<ApplicationCore.Entities.Product> product,
            IRepository<Order> order,
            IRepository<OrderPetDetail> orderpetdetail,
            IRepository<OrderCancel> ordercancel,
            IRepository<OrderSchedule> orderschedule,
            IRepository<Member> member,
            IRepository<Evaluation> evaluation,
            IRepository<UserContact> usercontact,
            IRepository<OfficialContact> officialcontact,
            IAccountManager accountManager)
        {
            _registersitter = registersitter;
            _product = product;
            _order = order;
            _orderpetdetail = orderpetdetail;
            _ordercancel = ordercancel;
            _orderschedule = orderschedule;
            _member = member;
            _evaluation = evaluation;
            _usercontact = usercontact;
            _officialcontact = officialcontact;
            _accountManager = accountManager;
            memberid = _accountManager.GetLoginMemberId();
        }


        #region 訂單(列表、明細、取消、siderbar)

        //訂單列表 
        public TransResultDto<List<SitterOrderListDto>> GetOrderList()
        {
            var order = _order.GetAllReadOnly().Where(o => o.SitterId == memberid).ToList();
            
            var orderlists= order.Select(o => new SitterOrderListDto
                               {
                                   OrderID = o.OrderId,
                                   OrderNumber = o.OrderNumber,
                                   SitterID = o.SitterId,
                                   CustomerID = o.CustomerId,
                                   PaymentStatus = (OrderStatus)o.OrderStatus,
                                   SitterName = o.SitterName,
                                   ProductName = o.ProductName,
                                   ProductImageUrl = o.ProductImageUrl,
                                   TotalPrice = o.Amount,
                                   ServiceDate = o.BeginTime
                               }).ToList();

            return SitterCenterResponseHelper.ReadResponse(orderlists);
        }

        //訂單明細
        public TransResultDto<SitterOrderDetailDto> GetOrderDetail(string ordernumber)
        {
            //取得訂單
            var order = _order.GetAllReadOnly().First(o => o.OrderNumber == ordernumber);
            //取得所有寵物資料
            var pet = _orderpetdetail.GetAllReadOnly().Where(p => p.OrderId == order.OrderId)
                .Select(p => new SitterOrderPetDto
                {
                    PetName = p.PetName,
                    PetSex = p.PetSex ? "男生" : "女生",
                    PetType = p.PetType,
                    ShapeType = p.ShapeType,
                    Description = p.PetDiscription,
                    BirthYear = p.BirthYear,
                    BirthMonth = p.BirthMonth,
                    Ligation = p.Ligation ? "是" : "否",
                    Vaccine = p.Vaccine ? "是" : "否",
                    Remark = p.PetIntro
                }).ToList();
            //取得cancel資料
            var cancel = new SitterOrderCancelDto();
            var notcancel = _ordercancel.GetAllReadOnly().Any(c => c.OrderId == order.OrderId);
            if (notcancel) 
            {cancel = _ordercancel.GetAllReadOnly().Where(c => c.OrderId == order.OrderId).Select(c => new SitterOrderCancelDto
                {
                    OrderId = order.OrderId,
                    CancelDate = c.CancelDate,
                    CancelReason = c.CancelReason
                }).First();}
            //取得sidebar資料
            var side = GetSideBar(order.OrderId);
           
            //合併
            var detail = new SitterOrderDetailDto
            {
                OrderID = order.OrderId,
                OrderNumber = order.OrderNumber,
                SitterID = order.SitterId,
                CustomerID = order.CustomerId,
                CreateTime = order.CreateTime,
                OrderStatus = (OrderStatus)order.OrderStatus,
                TotalPrice = order.Amount,
                InvoiceNumber = order.InvoiceId,
                ServiceTime = order.BeginTime,
                BeginTime = order.BeginTime,
                EndTime = order.EndTime,
                ProductIntro = order.ProductIntro,
                SitterName = order.SitterName,
                ProductImageUrl = order.ProductImageUrl,
                ServiceType = order.ProductName,
                ServiceTypeIntro = SitterCenterResponseHelper.GetServiceType(_product.GetAllReadOnly().First(p => p.ProductId == order.ProductId).ServiceType),
                BookingName = order.BookingName,
                BookingPhone = order.BookingPhone,
                BookingEmail = order.BookingEmail,
                Addredss = order.Address,
                Name = order.Name,
                Phone = order.Phone,
                PetDetails = pet,
                CancelDetail= cancel,
                SideBar=side
            };
            return SitterCenterResponseHelper.ReadResponse(detail);
        }

        //取消訂單
        public async Task<TransResultDto<SitterOrderCancelDto>> CancelOrder(SitterOrderCancelDto request)
        {
            var response = new TransResultDto<SitterOrderCancelDto>();
            var cancel = new OrderCancel 
            {
                OrderId=request.OrderId,
                CancelDate=request.CancelDate,
                CancelReason=request.CancelReason,
                RefundPersent=1m
                
            };
            var order = _order.GetById(request.OrderId);
            order.OrderStatus = (int)OrderStatus.Cancel;
            try
            {
                //新增cancel表、修改order狀態
                await _ordercancel.AddAsync(cancel);
                await _order.UpdateAsync(order);
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.Message = "Failed";
                return response;
            }
        }



        //組裝sidebar
        public SitterOrderSiderBarViewModel GetSideBar(int orderid) 
        {
            //取得訂單
            var order = _order.GetById(orderid);
            //取得所有寵物資料
            var pet = _orderpetdetail.GetAllReadOnly().Where(p => p.OrderId == orderid)
                .Select(p => new SitterOrderPetDto
                {
                    PetName = p.PetName,
                    PetSex = p.PetSex ? "男生" : "女生",
                    PetType = p.PetType,
                    ShapeType = p.ShapeType,
                    Description = p.PetDiscription,
                    BirthYear = p.BirthYear,
                    BirthMonth = p.BirthMonth,
                    Ligation = p.Ligation ? "是" : "否",
                    Vaccine = p.Vaccine ? "是" : "否",
                    Remark = p.PetIntro
                }).ToList();
            var petgroup = pet.GroupBy(p => p.PetType);
            var petlist = new List<OrderSidebarPetData>();
            foreach (var pettype in petgroup)
            {
                var petGroupShapeType = pet.Where(x => x.PetType == pettype.Key).GroupBy(x => x.ShapeType);
                foreach (var petshape in petGroupShapeType)
                {
                    var petcount = pet.Where(x => x.PetType == pettype.Key && x.ShapeType == petshape.Key).Count();
                    petlist.Add(new OrderSidebarPetData
                    {
                        PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)pettype.Key),
                        ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)petshape.Key),
                        PetCount = petcount
                    });
                }
            }
            //組裝sidebar
            return  new SitterOrderSiderBarViewModel
            {
                OrderID = orderid,
                OrderNumber = order.OrderNumber,
                SitterName = order.SitterName,
                ProductImageUrl = order.ProductImageUrl,
                BegineDate = order.BeginTime,
                ServiceTime = $"{order.BeginTime.ToString("HH:mm")}~{order.EndTime.ToString("HH:mm")}",
                ServiceType = order.ProductName,
                TotolPrice = order.Amount,
                OrderSidebarPetData = petlist
            };
        }


        #endregion

        #region 寄送email

        public EmailOrderDTO GetEmailOrderDTO(int orderId, int userType)
        {
            var order = _order.GetById(orderId);
            var email = new EmailOrderDTO
            {
                ContentDTO = GetEmailContentDTO(order, userType),
                SitterName = order.SitterName,
                MemberName = order.BookingName,
                ServiceName = order.ProductName,
                OrderNum = order.OrderNumber,
                ServiceDate = order.BeginTime.ToString("yyyy-MM-dd"),
                ServiceTime = $"{order.BeginTime.ToString("HH:mm")}-{order.EndTime.ToString("HH:mm")}",
            };
            if (order.OrderStatus == (int)OrderStatus.Cancel)
            {
                var ordercancel = GetOrderCancelDTO(orderId);
                email.EmailCancelOrderDTO = new EmailCancelOrderDTO
                {
                    CancelReason = ordercancel.CancelReason,
                    CreatrTime = ordercancel.CancelDate.ToString("yyyy-MM-dd HH:mm"),
                    CancelBackAmount = Math.Round(order.Amount * ordercancel.Persent)
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
                ContentDTO.UserName = order.BookingEmail;
            }
            else
            {
                ContentDTO.UserName = order.SitterName;
                ContentDTO.UserName = _member.GetById(order.SitterId).Email;
            }
            return ContentDTO;
        }


        private OrderCancelDTO GetOrderCancelDTO(int orderId)
        {
            var dto = _ordercancel.GetAllReadOnly().FirstOrDefault(x => x.OrderId == orderId);
            if (dto != null)
            {
                var cancelDto = new OrderCancelDTO
                {
                    OrderId = orderId,
                    CancelDate = dto.CancelDate,
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

        #endregion

        #region 聊天室(顧客)

        //取出所有聊天紀錄
        private List<ContactCustomerDto> GetCustomerChat(int userId)
        {
            var chats = from u in _usercontact.GetAllReadOnly()
                                  join s in _registersitter.GetAllReadOnly() on u.SitterId equals s.MemberId
                                  join sm in _member.GetAllReadOnly() on s.MemberId equals sm.MemberId
                                  join m in _member.GetAllReadOnly() on u.MemberId equals m.MemberId
                                  where u.SitterId == userId
                                  orderby u.CreateTime descending
                                  select new ContactCustomerDto
                                  {
                                      MemberId = u.MemberId,
                                      MemberImage = m.ProfileImage,
                                      MemberName = m.Name,
                                      SitterId = u.SitterId,
                                      SitterImage = s.SitterPicture==null? sm.ProfileImage:s.SitterPicture,
                                      Message = u.Message,
                                      CreateTime = u.CreateTime,
                                      IsMember = u.IsMemberSpeak
                                  };
            return chats.ToList();
        }

        //分類製作成聊天室
        public IEnumerable<CustomerChatViewModel> GetCustomerChatroom()
        {
            var chats = GetCustomerChat(memberid);
            var chatrooms = chats.GroupBy(x => x.MemberId).ToList();

            foreach (var item in chatrooms)
            {
                var firstMessage = item.First();
                var chatroomVM = new CustomerChatViewModel
                {
                    CustomerID = firstMessage.MemberId,
                    CustomerName = firstMessage.MemberName,
                    LastestContext = firstMessage.Message,
                    CreateTime = firstMessage.CreateTime
                };
                if (item.First().IsMember == true)  //如果是顧客發話
                {
                    chatroomVM.IsMember = true;
                    chatroomVM.ImageUrl = firstMessage.MemberImage;
                    yield return chatroomVM;
                }
                else
                {
                    chatroomVM.IsMember = false;
                    chatroomVM.ImageUrl = firstMessage.SitterImage;
                    yield return chatroomVM;
                }
            }
        }

        //取個別聊天室
        public CustomerContactViewModel GetCustomerContactDetail(int customerid)
        {
            var customer = _member.GetAllReadOnly().FirstOrDefault(x => x.MemberId == customerid);
            if (customer is null)
            {
                return null;
            }

            var chats = GetCustomerChat(memberid).Where(x => x.MemberId == customerid);
            var chatroomDetail = new List<SitterContactDto>();

            foreach (var item in chats.ToList())
            {
                if (item.IsMember == true)
                {
                    chatroomDetail.Add(new SitterContactDto
                    {
                        IsUserType = true,
                        UserId = item.MemberId,
                        UserImage = item.MemberImage,
                        Message = item.Message,
                        CreateTime = item.CreateTime
                    });
                }
                else
                {
                    chatroomDetail.Add(new SitterContactDto
                    {
                        IsUserType = false,
                        UserId = item.SitterId,
                        UserImage = item.SitterImage,
                        Message = item.Message,
                        CreateTime = item.CreateTime
                    });
                }
            }

            var viewmodel = new CustomerContactViewModel
            {
                CustomerNamer = customer.Name,
                CustomerId = customer.MemberId
            };
            if (chatroomDetail.Count != 0)
            {
                viewmodel.Contact = chatroomDetail;
            }
            return viewmodel;

        }

        //建立聊天內容
        public async Task<ResultDto> CreateCustomerContactDetail(string message, int customer)
        {
            var response = new ResultDto();
            var contact = new UserContact
            {
                MemberId = customer,
                SitterId = memberid,
                Message = message,
                CreateTime = DateTime.UtcNow,
                IsMemberSpeak = false
            };

            try
            {
                await _usercontact.AddAsync(contact);
                var result = new SitterChatDto
                {
                    IsSuccess = true,
                    Time = contact.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm")
                };
                return new ResultDto(result);
            }
            catch(Exception ex)
            {
                response.Message = ex.ToString();
                return response;
            }
        }

        #endregion

        #region 聊天室(官方)

        //取出所有聊天紀錄
        private List<OfficialChatViewModel> GetOfficialChat(int userId)
        {
            var chat = from of in _officialcontact.GetAllReadOnly()
                                   join o in _order.GetAllReadOnly() on of.OrderId equals o.OrderId
                                   join m in _member.GetAllReadOnly() on of.UserId equals m.MemberId
                                   where of.UserId == userId && of.UserType == (int)UserType.Sitter
                                   orderby of.CreateTime descending
                                   select new OfficialChatViewModel
                                   {
                                       IsUserSpeak = of.IsUserSpeak,
                                       OrderID = (int)of.OrderId,
                                       UserId = of.UserId,
                                       OrderNumber = o.OrderNumber,
                                       ServiceType = o.ProductName,
                                       ImageUrl = m.ProfileImage,
                                       LastestContext = of.Message,
                                       CreateTime = of.CreateTime
                                   };
            return chat.ToList();
        }

        //分類製作成聊天室
        public IEnumerable<OfficialChatViewModel> GetOfficialChatroom()
        {
            var chatrooms = new List<OfficialChatViewModel>();

            //取得所有聊天紀錄、整理成chatroom
            var chats = GetOfficialChat(memberid);
            var chatroomList = chats.GroupBy(x => x.OrderID).ToList();
            foreach (var item in chatroomList)
            {
                chatrooms.Add(item.First());
            }

            //官方訊息(非訂單詢問)
            var officialmessage = _officialcontact.GetAllReadOnly()
                .Where(x => x.UserId == memberid && x.OrderId == null && x.UserType == (int)UserType.Sitter)
                .Select(x => new OfficialChatViewModel
                {
                    UserId = x.UserId,
                    LastestContext = x.Message,
                    CreateTime = x.CreateTime
                }); 
            foreach (var item in officialmessage)
            {
                chatrooms.Add(item);
            }
            
            return chatrooms.OrderByDescending(x => x.CreateTime).ToList();
        }

        //取得個別聊天室
        public OfficialContactViewModel GetOfficialContactDetail(string id)
        {
            var order = _order.GetAllReadOnly().Where(o=>o.OrderNumber==id).First();
            var chats = GetOfficialChat(order.SitterId).Where(x => x.OrderID == order.OrderId).Select(x=>new SitterContactDto
            {
                IsUserType = x.IsUserSpeak,
                UserId = order.SitterId,
                UserImage = x.ImageUrl,
                Message = x.LastestContext,
                CreateTime = x.CreateTime,
            }).ToList();

            var OfficialContactDetail = new OfficialContactViewModel
            {
                Contact = chats,
                OrderNum = order.OrderNumber,
                OrderID = order.OrderId,
                SideBar=GetSideBar(order.OrderId)
            };

            return OfficialContactDetail;
        }

        //建立聊天內容
        public async Task<ResultDto> CreateOfficialContactDetail(string message,int orderid)
        {
            var response = new ResultDto();
            var contact = new OfficialContact
            {
                OrderId = orderid,
                UserType = (int)UserType.Sitter,
                UserId = memberid,
                Message = message,
                CreateTime = DateTime.UtcNow,
                IsUserSpeak = true
            };
            
            try
            {
                await _officialcontact.AddAsync(contact);
                var sitter = _registersitter.GetAllReadOnly().FirstOrDefault(x => x.MemberId == memberid);
                string img;
                if (sitter != null) { img = sitter.SitterPicture; }
                else { img = _member.GetById(memberid).ProfileImage; }
                var result = new SitterChatDto 
                {
                    IsSuccess=true,
                    Time = contact.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                    Image = img
                };
                return new ResultDto(result);
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();
                return response;
            }
        }


        #endregion

        #region 評論
        private bool CheckOrderEvaluation(Order order)
        {
            var serviceTime = _orderschedule.GetAllReadOnly().First(x => x.OrderId == order.OrderId).ServiceDate;
            if (serviceTime > DateTime.Now || order.OrderStatus == (int)OrderStatus.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //取得評論
        public TransResultDto<SitterOrderEvaluationDto> GetEvaluation(string ordernumber)
        {
            var response = new TransResultDto<SitterOrderEvaluationDto>();
            var order = _order.GetAllReadOnly().First(o=>o.OrderNumber==ordernumber);
            var sidebar = GetSideBar(order.OrderId);
            bool IsSuccess = CheckOrderEvaluation(order);
            if (!IsSuccess)
            {
                return null;
            }
            var evaluation = _evaluation.GetAllReadOnly().FirstOrDefault(x => x.OrderId == order.OrderId && x.UserType == (int)UserType.Sitter);
            if (evaluation is null)
            {
                var data = new SitterOrderEvaluationDto() { OrderNumber = order.OrderNumber, Orderid = order.OrderId, Sidebar= sidebar };
                return SitterCenterResponseHelper.ReadResponse(data);
            }
            else
            {
                var orderEvaluation = new SitterOrderEvaluationDto
                {
                    Orderid= order.OrderId,
                    OrderNumber=order.OrderNumber,
                    EvaluationScore=evaluation.EvaluationScore,
                    EvaluationMessage=evaluation.Message,
                    CreateTime=evaluation.CreateTime,
                    Sidebar= sidebar
                };
                return SitterCenterResponseHelper.ReadResponse(orderEvaluation);
            }
        }
        //建立評論
        public async Task<TransResultDto<SitterOrderEvaluationDto>> CreateEvaluation(SitterOrderEvaluationDto request)
        {
            var response = new TransResultDto<SitterOrderEvaluationDto>();

            var evaluation = new Evaluation
            {
                OrderId = request.Orderid,
                UserId = memberid,
                UserType = (int)UserType.Sitter,
                EvaluationScore = request.EvaluationScore,
                Message = request.EvaluationMessage,
                CreateTime = request.CreateTime
            };

            try
            {
                await _evaluation.AddAsync(evaluation);
                response.IsSuccess = true;
                return response;
            }
            catch
            {
                response.Message = "Failed";
                return response;
            }
        }

        //歷史評論
        public TransResultDto<List<SitterOrderEvaluationDto>>  GetHistoryEvaluation()
        {
            var evaluations = from e in _evaluation.GetAllReadOnly()
                              join o in _order.GetAllReadOnly() on e.OrderId equals o.OrderId
                              join s in _registersitter.GetAllReadOnly() on o.SitterId equals s.MemberId
                              where e.OrderId == o.OrderId
                              && e.UserType == (int)UserType.Member && o.SitterId == memberid
                              orderby e.CreateTime descending
                              select new SitterOrderEvaluationDto()
                              {
                                  Orderid=e.OrderId,
                                  OrderNumber = o.OrderNumber,
                                  UserImageUrl = s.SitterPicture,
                                  EvaluationScore = e.EvaluationScore,
                                  EvaluationMessage = e.Message,
                                  CreateTime = e.CreateTime
                              };

            var evaluation = evaluations.ToList();
            return SitterCenterResponseHelper.ReadResponse(evaluation);
        }

        #endregion

        #region 行事曆
        //行事曆
        public ResultDto GetCalenderList()
        {
            //把全部的日期傳出去
            //訂單編號、日期、完整時間、服務類型、地址
            var result = new ResultDto();
            var datelist = _order.GetAllReadOnly().Where(o => o.SitterId == memberid && o.OrderStatus!=(int)OrderStatus.Cancel).Select(o => new CalenderDto
            {
                OrderId = o.OrderId,
                OrderNumber = o.OrderNumber,
                BeginTime = o.BeginTime,
                EndTime = o.EndTime,
                ServiceType = o.ProductName,
                Address = o.Address
            }).ToList();
            if (datelist.Count == 0)
            {
                result.Message = "Not Found";
                return result;
            }

            result.IsSuccess = true;
            result.Data = datelist;
            return result;
        }

        #endregion
    }
}

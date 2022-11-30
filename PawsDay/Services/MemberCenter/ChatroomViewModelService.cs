using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.VisualBasic;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDay.Services.MemberCenter
{
    public class ChatroomViewModelService
    {
        private readonly IRepository<UserContact> _userContact;
        private readonly IRepository<RegisterSitter> _registerSitter;
        private readonly IRepository<Order> _order;
        private readonly IRepository<OfficialContact> _officalContact;
        private readonly IRepository<Member> _member;
    
    public ChatroomViewModelService(IRepository<UserContact> userContact, IRepository<RegisterSitter> registerSitter, IRepository<Order> order, IRepository<OfficialContact> officalContact, IRepository<Member> member, IRepository<RegisterSitter> segisterSitter)
        {
            _userContact = userContact;
            _registerSitter = registerSitter;
            _order = order;
            _officalContact = officalContact;
            _member = member;
        }
        #region 聊天室-保姆
        private List<ContactSitterDTO> GetChatroomSisterList(int userId)
        {
            var chatroomSisters = from u in _userContact.GetAllReadOnly()
                                  join s in _registerSitter.GetAllReadOnly() on u.SitterId equals s.MemberId
                                  join m in _member.GetAllReadOnly() on u.MemberId equals m.MemberId
                                  where u.MemberId == userId
                                  orderby u.CreateTime descending
                                  select new ContactSitterDTO
                                  {
                                      MemberId = u.MemberId,
                                      MemberImage = m.ProfileImage,
                                      SitterId = u.SitterId,
                                      SitterName = s.SitterName,
                                      SitterImage = s.SitterPicture,
                                      Message = u.Message,
                                      CreateTime = u.CreateTime.AddHours(8),
                                      IsMember = u.IsMemberSpeak
                                  };
            return chatroomSisters.ToList();
        }

        public IEnumerable<ChatroomSisterViewModel> GetChatroomSister(int userId)
        {
            var chatroomSisters = GetChatroomSisterList(userId);

            var chatroomSistersLists = chatroomSisters.GroupBy(x => x.SitterId).ToList();

            foreach (var item in chatroomSistersLists)
            {
                var firstMessage = item.First();
                var chatroomSister = new ChatroomSisterViewModel
                {
                    SitterId = firstMessage.SitterId,
                    SittertName = firstMessage.SitterName,
                    Message = firstMessage.Message,
                    EditTime = firstMessage.CreateTime.AddHours(8)
                };
                if (item.First().IsMember == true)
                {
                    chatroomSister.IsMember = true;
                    chatroomSister.UserImage = firstMessage.MemberImage;
                    yield return chatroomSister;
                }
                else
                {
                    chatroomSister.IsMember = false;
                    chatroomSister.UserImage = firstMessage.SitterImage;
                    yield return chatroomSister;
                }
            }
        }

        public ChatroomSisterDetailViewModel GetChatroomSisterDetail(int userId, int sitterId)
        {
            var sitter=_registerSitter.GetAllReadOnly().FirstOrDefault(x=>x.MemberId==sitterId);
            if (sitter is null)
            {
                return null;
            }
            var chatroomSisters = GetChatroomSisterList(userId).Where(x => x.SitterId == sitterId);
            var chatroomSisterDetail = new List<ContactListDTO>();

            foreach (var item in chatroomSisters.ToList())
            {
                if (item.IsMember == true)
                {
                    chatroomSisterDetail.Add(new ContactListDTO
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
                    chatroomSisterDetail.Add(new ContactListDTO
                    {
                        IsUserType = false,
                        UserId = item.SitterId,
                        UserImage = item.SitterImage,
                        Message = item.Message,
                        CreateTime = item.CreateTime
                    });
                }
            }

            var sitterviewmodel = new ChatroomSisterDetailViewModel
            {
                SitterName = _registerSitter.GetAllReadOnly().Where(x => x.MemberId == sitterId).First().SitterName,
                SitterId = sitterId
            };
            if (chatroomSisterDetail.Count != 0)
            {
                sitterviewmodel.Contact = chatroomSisterDetail;
            }
            return sitterviewmodel;

        }

        public async Task<ResultDto> CreateSisterDetail(string message, int userId, int sitterId)
        {
            var sisterDetail = new UserContact
            {
                MemberId = userId,
                SitterId = sitterId,
                Message = message,
                CreateTime = DateTime.UtcNow,
                IsMemberSpeak = true
            };
            var dto = new ChatroomDetailDTO();
             
            try
            {
                await _userContact.AddAsync(sisterDetail);
                dto.IsSuccess = true;
                dto.Time = sisterDetail.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm");
                return new ResultDto(dto);
            }
            catch
            {
                dto.IsSuccess = false;
                return new ResultDto(dto);
            }
           
        }
        #endregion

        #region 聊天室-官網
        private List<ChatroomPawsdayViewModel> GetChatroomPawsdayList(int userId)
        {
            var chatroomPawsdays = from of in _officalContact.GetAllReadOnly()
                                   join o in _order.GetAllReadOnly() on of.OrderId equals o.OrderId
                                   join m in _member.GetAllReadOnly() on of.UserId equals m.MemberId
                                   where of.UserId == userId && of.UserType == (int)UserType.Member
                                   orderby of.CreateTime descending
                                   select new ChatroomPawsdayViewModel
                                   {
                                       IsUserSpeak = of.IsUserSpeak,
                                       OrderId = (int)of.OrderId,
                                       UserId = of.UserId,
                                       OrderNumber = o.OrderNumber,
                                       ServiceName = o.ProductName,
                                       UserImage = m.ProfileImage,
                                       Message = of.Message,
                                       CreateTime = of.CreateTime.AddHours(8)
                                   };
            return chatroomPawsdays.ToList();
        }

        public IEnumerable<ChatroomPawsdayViewModel> GetChatroomPawsday(int userId)
        {
            var chatroomPawsdays = GetChatroomPawsdayList(userId);

            var chatroomPawsdaysOrderNull = _officalContact.GetAllReadOnly().Where(x => x.UserId == userId && x.OrderId == null && x.UserType == (int)UserType.Member).Select(x => new ChatroomPawsdayViewModel
            {
                UserId = x.UserId,
                Message = x.Message,
                CreateTime = x.CreateTime.AddHours(8)
            }) ;

            var chatroomPaws = new List<ChatroomPawsdayViewModel>();

            foreach (var item in chatroomPawsdaysOrderNull)
            {
                chatroomPaws.Add(item);
            }           

            var chatroomSistersLists = chatroomPawsdays.GroupBy(x => x.OrderId).ToList();

            foreach (var item in chatroomSistersLists)
            {
                chatroomPaws.Add(item.First());
            }
            return chatroomPaws.OrderByDescending(x => x.CreateTime).ToList();
        }

        public OrderContactViewModel GetOrderContactDetail(int orderId)
        {
            var userId = GetOrderUserId(orderId);
            var orderContact = GetChatroomPawsdayList(userId).Where(x => x.OrderId == orderId).ToList();
            List<ContactListDTO> orderContactDTO = new List<ContactListDTO>();
            foreach (var item in orderContact)
            {
                orderContactDTO.Add(new ContactListDTO
                {
                    IsUserType = item.IsUserSpeak,
                    UserId = userId,
                    UserImage = item.UserImage,
                    Message = item.Message,
                    CreateTime = item.CreateTime,
                });
            }
            var orderContactDetail = new OrderContactViewModel
            {
                Contact = orderContactDTO,
                OrderNum = _order.GetById(orderId).OrderNumber,
                OrderID = orderId,
            };

            return orderContactDetail;

        }

        public async Task<ResultDto> CreateOrderContact(OrderContactDTO input)
        {
            var userId = GetOrderUserId(input.OrderID);
            var orderContact = new OfficialContact
            {
                OrderId = input.OrderID,
                UserType = (int)UserType.Member,
                UserId = userId,
                Message = input.Message,
                CreateTime = DateTime.UtcNow,
                IsUserSpeak = true
            };
            var dto = new ChatroomDetailDTO();
            try
            {
                await _officalContact.AddAsync(orderContact);
                dto.IsSuccess = true;
                dto.Time = orderContact.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm");
                dto.Image=_member.GetAllReadOnly().First(x=>x.MemberId==userId).ProfileImage;
                return new ResultDto(dto);
            }
            catch
            {
                dto.IsSuccess = false;
                return new ResultDto(dto);
            }           

        }
        private int GetOrderUserId(int orderId)
        {
            return _order.GetById(orderId).CustomerId;
        }

        #endregion
    }

}
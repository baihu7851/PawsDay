using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Message;
using PawsDayBackEnd.Models;
using PawsDayBackEnd.Services.SendGrid;
using PawsDayBackEnd.Services.SendGrid.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PawsDayBackEnd.Services
{
    public class MessageServices
    {
        private readonly IRepository<Member> _member;
        private readonly IRepository<Contact> _contact;
        private readonly IRepository<OfficialContact> _officialContact;
        private readonly SendGridServices _sendGridService;
        private readonly IRepository<Order> _order;
        public MessageServices(IRepository<Member> member, IRepository<Contact> contact, IRepository<OfficialContact> officialContact, SendGridServices sendGridService, IRepository<Order> order)
        {
            _member = member;
            _contact = contact;
            _officialContact = officialContact;
            _sendGridService = sendGridService;
            _order = order;
        }

        #region 連絡我們
        public ApiResultDto GetALLContact(int currentPage, int perPage)
        {
            var contactList = _contact.GetAllReadOnly().OrderByDescending(x => x.ContactId).ToList();
            var response = new ContactVM
            {
                Contact = contactList.Skip(currentPage * perPage).Take(perPage).ToList(),
                TotalCount = contactList.Count,
            };
            return new ApiResultDto(response);
        }
        public ApiResultDto GetSearchContact(string name)
        {
            var contactList = _contact.GetAllReadOnly().Where(x => x.Name == name).OrderByDescending(x => x.ContactId).ToList();
            var response = new ContactVM
            {
                Contact = contactList,
                TotalCount = contactList.Count,
            };
            return new ApiResultDto(response);
        }

        public ApiResultDto CreateContactAnswer(ContactDTO answer)
        {
            var result = new ResultDto();
            var contact = _contact.GetById(answer.ContactId);
            try
            {
                contact.ReplyContent = answer.ContactAnswer;
                contact.ReplyTime = DateTime.UtcNow;
                contact.Status = true;
                _contact.Update(contact);
                result.Success = true;
                result.Message = "新增成功";
                EmailAnswerContactUs(contact);
            }
            catch
            {
                result.Success = false;
                result.Message = "新增失敗";
            }

            return new ApiResultDto(result);
        }
        private async void EmailAnswerContactUs(Contact user)
        {
            var email = new EmailContactUsDTO
            {
                //EmailContentDTO = new EmailContentDTO { UserName=user.Name,UserEmail=user.Email},
                EmailContentDTO = new EmailContentDTO { UserName = user.Name, UserEmail = user.Email },
                ContactTitle = user.Title,
                ContactContent = user.ContactContent,
                AnswerContent = user.ReplyContent
            };
            await _sendGridService.AnswerContactUs(email);
        }
        #endregion

        #region 訂單問題
        
        public ApiResultDto GetOrderContact(int currentPage, int perPage, int type)
        {
            var list = _officialContact.GetAllReadOnly().
                    Where(x => x.UserType == type && x.OrderId != null).OrderByDescending(x => x.OfficialContactId).ToList().GroupBy(x => x.OrderId);

            var officialContact = list.Skip(currentPage * perPage).Take(perPage);

            var response = new ContactOFVM
            {
                Contact = GetOrderContactList(officialContact),
                TotalCount = list.Count(),
            };
            return new ApiResultDto(response);
        }

        public ApiResultDto GetOrderContactSearchOrderId(int currentPage, int perPage, int type,string searchtype,string input)
        {
            int orderId=0;
            if (searchtype != "ID")
            {
                orderId = GetOrderId(input);
            }
            else
            {                
                bool success = int.TryParse(input, out orderId);
            }
            var list = _officialContact.GetAllReadOnly().
                     Where(x => x.UserType == type && x.OrderId != null && x.OrderId == orderId).OrderByDescending(x => x.OfficialContactId).ToList().GroupBy(x => x.OrderId);

            var officialContact = list.Skip(currentPage * perPage).Take(perPage);

            var response = new ContactOFVM
            {
                Contact = GetOrderContactList(officialContact),
                TotalCount = list.Count(),
            };
            return new ApiResultDto(response);
        }

        public ApiResultDto GetOrderContactSearchMemberId(int currentPage, int perPage, int type, int input)
        {
            
            var list = _officialContact.GetAllReadOnly().
                     Where(x => x.UserType == type && x.OrderId != null && x.UserId==input).OrderByDescending(x => x.OfficialContactId).ToList().GroupBy(x => x.OrderId);

            var officialContact = list.Skip(currentPage * perPage).Take(perPage);

            var response = new ContactOFVM
            {
                Contact = GetOrderContactList(officialContact),
                TotalCount = list.Count(),
            };
            return new ApiResultDto(response);
        }
        public ApiResultDto GetOrderContactSearchMemberName(int currentPage, int perPage, int type, string input)
        {

            var contactList = (from of in _officialContact.GetAllReadOnly()
                       join m in _member.GetAllReadOnly() on of.UserId equals m.MemberId
                       where of.UserType==type && of.OrderId != null && m.Name==input
                       orderby of.OfficialContactId
                       select of).ToList();

            var list = contactList.GroupBy(x => x.OrderId).ToList();

            var officialContact = list.Skip(currentPage * perPage).Take(perPage);

            var response = new ContactOFVM
            {
                Contact = GetOrderContactList(officialContact),
                TotalCount = list.Count(),
            };
            return new ApiResultDto(response);
        }
       

        public ApiResultDto CreateOrderAnswer(OrderContactAnswerDTO answer)
        {
            var result = new ResultDto();
            var contact = new OfficialContact
            {
                OrderId = answer.OrderId,
                UserType = answer.UserType,
                UserId = answer.UserId,
                Message = answer.Message,
                CreateTime = DateTime.UtcNow,
                IsUserSpeak = false
            };
            try
            {
                _officialContact.Add(contact);
                result.Success = true;
                result.Message = "新增成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增失敗";
            }

            return new ApiResultDto(result);
        }

        private List<OrderContactDTO> GetOrderContactList(IEnumerable<IGrouping<int?, OfficialContact>> officialContact)
        {
            var respone = new List<OrderContactDTO>();
            foreach (var contactg in officialContact)
            {
                var orderContact = contactg.First();
                var contactglist = new OrderContactDTO
                {
                    OrderId = (int)orderContact.OrderId,
                    OrderNum = _order.GetById(orderContact.OrderId).OrderNumber,
                    UserId = orderContact.UserId,
                    UserName = _member.GetById(orderContact.UserId).Name,
                    LastAnswerIsUser = orderContact.IsUserSpeak,
                    OrderContent = new List<OrderContactContentDTO>(),
                };
                foreach (var contact in contactg.OrderByDescending(x => x.CreateTime))
                {
                    var contactlist = new OrderContactContentDTO
                    {
                        OfficialContactId = contact.OfficialContactId,
                        Message = contact.Message,
                        CreateTime = contact.CreateTime.AddHours(8),
                        IsUserSpeak = contact.IsUserSpeak,
                    };
                    contactglist.OrderContent.Add(contactlist);
                }
                respone.Add(contactglist);
            }
            return respone;
        }

        private int GetOrderId(string num)
        {
            var order = _order.GetAllReadOnly().FirstOrDefault(x => x.OrderNumber == num);
            if (order != null)
            {
                return order.OrderId;
            }
            else
            { 
                return 0;
            }            
        }
        
        #endregion

    }
}

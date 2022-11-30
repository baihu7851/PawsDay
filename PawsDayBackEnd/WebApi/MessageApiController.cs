using ApplicationCore.Constants;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Message;
using PawsDayBackEnd.Services;
using SendGrid;

namespace PawsDayBackEnd.WebApi
{
    [Authorize(Roles = AuthorizationConstants.Administrator)]
    [ApiController]
    public class MessageApiController : BaseApiController
    {
        private readonly MessageServices _messageServices;

        public MessageApiController(MessageServices messageServices)
        {
            _messageServices = messageServices;
        }
        #region 聯絡我們
        [HttpGet]
        public ApiResultDto GetALLContact(int currentPage, int perPage)
        {
            var response = _messageServices.GetALLContact(currentPage, perPage);
            var checkResponse = CheckResponse(response);
            return checkResponse;
        }

        [HttpPost]
        public ApiResultDto CreateContactAnswer([FromBody] ContactDTO result)
        {
            var response = _messageServices.CreateContactAnswer(result);
            return response;
        }

        [HttpGet]
        public ApiResultDto GetSearchContact(string name)
        {
            var response = _messageServices.GetSearchContact(name);
            var checkResponse = CheckResponse(response);
            return checkResponse;
        }
        
        #endregion

        #region 訂單問題
        [HttpGet]
        public ApiResultDto GetOrderContact(int currentPage, int perPage, int type)
        {
            var response = _messageServices.GetOrderContact(currentPage, perPage, type);
            var checkResponse = CheckResponse(response);
            return checkResponse;
        }
        [HttpGet]
        public ApiResultDto GetOrderContactOrderId(int currentPage, int perPage, int type, string searchtype, string input)
        {
            var response = _messageServices.GetOrderContactSearchOrderId(currentPage,perPage,type,searchtype,input);
            var checkResponse = CheckResponse(response);
            return checkResponse;
        }

        [HttpGet]
        public ApiResultDto GetOrderContactMemberId(int currentPage, int perPage, int type, int input)
        {
            var response = _messageServices.GetOrderContactSearchMemberId(currentPage, perPage, type, input);
            var checkResponse = CheckResponse(response);
            return checkResponse;
        }
        [HttpGet]
        public ApiResultDto GetOrderContactMemberName(int currentPage, int perPage, int type, string input)
        {
            var response = _messageServices.GetOrderContactSearchMemberName(currentPage, perPage, type, input);
            var checkResponse = CheckResponse(response);
            return checkResponse;
        }


        [HttpPost]
        public ApiResultDto CreateOrderAnswer([FromBody] OrderContactAnswerDTO result)
        {
            var response = _messageServices.CreateOrderAnswer(result);
            return response;
        }
        #endregion


        private ApiResultDto CheckResponse(ApiResultDto response)
        {
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;

        }

    }


}

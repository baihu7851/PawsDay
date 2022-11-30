using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Order;
using PawsDayBackEnd.Services;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PawsDayBackEnd.WebApi
{
    [Authorize(Roles = AuthorizationConstants.Administrator)]
    
    public class OrderApiController : BaseApiController
    {
        private readonly OrderServices _orderServices;

        public OrderApiController(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet]
        public ApiResultDto GetALLOrderList(int currentPage, int perPage,int stauts)
        {
            var response = _orderServices.GetALLOrderList(currentPage, perPage, stauts);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderContent(int id)
        {
            var response = _orderServices.GetOrderContent(id);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderPetContent(int id)
        {
            var response = _orderServices.GetOrderPetContent(id);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderSearchNum(int type,string name)
        {
            var response = _orderServices.GetOrderSearchNum(type,name);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderSearchId(int id)
        {
            var response = _orderServices.GetOrderSearchId(id);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
       
        [HttpGet]
        public ApiResultDto GetOrderSearchNumStatusSuccess(int type, string name,int status)
        {
            var response = _orderServices.GetOrderSearchNumStatusSuccess(type, name,status);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        
        [HttpGet]
        public ApiResultDto GetOrderSuccessList(int currentPage, int perPage, int status)
        {
            var response = _orderServices.GetOrderSuccessList(currentPage,perPage, status);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderAlreadyList(int currentPage, int perPage, int status)
        {
            var response = _orderServices.GetOrderAlreadyList(currentPage, perPage, status);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        
        [HttpGet]
        public ApiResultDto GetOrderCancelList(int orderId)
        {
            var response = _orderServices.GetOrderCancelList(orderId);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderEvaluation(int orderId)
        {
            var response = _orderServices.GetOrderEvaluation(orderId);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpGet]
        public ApiResultDto GetOrderDate(int orderId)
        {
            var response = _orderServices.GetOrderDate(orderId);
            if (response is null)
            {
                response = new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
       
        [HttpPost]
        public ApiResultDto ChangeOrderSuccessList([FromBody] ChangeOrderStatusDTO input)
        {
            var response = _orderServices.ChangeOrderStatus(input);
            if (response is null)
            {
                return new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }

        [HttpPost]
        public ApiResultDto ChangeOrderHandleStatus([FromBody] ChangeOrderStatusDTO input)
        {
            var response = _orderServices.ChangeOrderHandleStatus(input);
            if (response is null)
            {
                return new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }
        [HttpPost]
        public ApiResultDto ChangeOrderStatusClear([FromBody] List<int> input)
        {
            var response = _orderServices.ChangeOrderStatusClear(input);
            if (response is null)
            {
                return new ApiResultDto { Status = DTO.StatusCode.Failed };
            }
            return response;
        }

       
    }
}

using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PawsDayBackEnd.Services
{
    public class OrderServices
    {
        private readonly IRepository<Member> _member;
        private readonly IRepository<Order> _order;
        private readonly IRepository<OrderPetDetail> _orderPet;
        private readonly IRepository<OrderCancel> _cancel;
        private readonly IRepository<Evaluation> _evaluation;

        public OrderServices(IRepository<Member> member, IRepository<Order> order, IRepository<OrderPetDetail> orderPet, IRepository<OrderCancel> cancel, IRepository<Evaluation> evaluation)
        {
            _member = member;
            _order = order;
            _orderPet = orderPet;
            _cancel = cancel;
            _evaluation = evaluation;
        }

        public ApiResultDto GetALLOrderList(int currentPage, int perPage,int status)
        {
            int total;
            var response = new List<OrderAllListDTO>();
            if (status < 0)
            {
                total = _order.GetAllReadOnly().ToList().Count();
                response = _order.GetAllReadOnly().OrderByDescending(x => x.OrderId).Skip(currentPage * perPage).Take(perPage).Select(x => new OrderAllListDTO
                {
                    OrderId = x.OrderId,
                    OrderName = x.OrderNumber,
                    UserName = x.BookingName,
                    Status = x.OrderStatus,
                    TotalPrice = x.Amount
                }).ToList();
            }
            else
            {
                total = _order.GetAllReadOnly().Where(x => x.OrderStatus == status).ToList().Count();
                response = _order.GetAllReadOnly().Where(x=>x.OrderStatus==status).OrderByDescending(x => x.OrderId).Skip(currentPage * perPage).Take(perPage).Select(x => new OrderAllListDTO
                {
                    OrderId = x.OrderId,
                    OrderName = x.OrderNumber,
                    UserName = x.BookingName,
                    Status = x.OrderStatus,
                    TotalPrice = x.Amount
                }).ToList();
            }
            
            
            var OrderAllListOFVM = new OrderAllListOFVM
            {
                Contact=response,
                TotalCount=total
            };

            return new ApiResultDto(OrderAllListOFVM);
        }
        public ApiResultDto GetOrderContent(int id)
        {
            var response = _order.GetById(id);           
                  

            return new ApiResultDto(response);
        }
        public ApiResultDto GetOrderPetContent(int id)
        {
            var response = _orderPet.GetAllReadOnly().Where(x => x.OrderId == id).ToList();

            return new ApiResultDto(response);
        }
        public ApiResultDto GetOrderSearchNum(int type,string name)
        {
            var order=new Order();
            if (type < 0)
            {
                order = _order.GetAllReadOnly().FirstOrDefault(x => x.OrderNumber == name);
            }
            else
            {
                order = _order.GetAllReadOnly().Where(x => x.OrderStatus == type).FirstOrDefault(x => x.OrderNumber == name);
            }           
            
            if (order is null)
            {                
                return new ApiResultDto();
            }
            else
            {              
                var list = new OrderAllListDTO
                {
                    OrderId = order.OrderId,
                    OrderName = order.OrderNumber,
                    UserName = order.BookingName,
                    Status = order.OrderStatus,
                    TotalPrice = order.Amount
                };
                var response = new OrderOFVM { TotalCount = 1,Contact=list };
                return new ApiResultDto(response);
            }           
        }
        public ApiResultDto GetOrderSearchId(int id)
        {
            var order =  _order.GetAllReadOnly().FirstOrDefault(x=>x.OrderId==id); 

            if (order is null)
            {
                return new ApiResultDto();
            }
            else
            {
                var list = new OrderAllListDTO
                {
                    OrderId = order.OrderId,
                    OrderName = order.OrderNumber,
                    UserName = order.BookingName,
                    Status = order.OrderStatus,
                    TotalPrice = order.Amount
                };
                var response = new OrderOFVM { TotalCount = 1, Contact = list };
                return new ApiResultDto(response);
            }

        }
       

        public ApiResultDto GetOrderEvaluation(int id)
        {
            var order = _evaluation.GetAllReadOnly().Where(x => x.OrderId == id).ToList();

            if (order.Count==0)
            {
                return new ApiResultDto();
            }
            else  
            {                
                return new ApiResultDto(order);
            }
        }
        public ApiResultDto GetOrderSearchNumStatusSuccess(int type, string name,int status)
        {
            var order = _order.GetAllReadOnly().Where(x => x.OrderStatus == type && DateTime.Compare(x.EndTime, DateTime.UtcNow)==status).FirstOrDefault(x => x.OrderNumber == name);

            if (order is null)
            {
                return new ApiResultDto();
            }
            else
            {
                var list = new OrderAllListDTO
                {
                    OrderId = order.OrderId,
                    OrderName = order.OrderNumber,
                    UserName = order.BookingName,
                    Status = order.OrderStatus,
                    TotalPrice = order.Amount
                };
                var response = new OrderOFVM { TotalCount = 1, Contact = list };
                return new ApiResultDto(response);
            }
        }
        public ApiResultDto GetOrderSuccessList(int currentPage, int perPage, int status)
        {
            int total = _order.GetAllReadOnly().Where(x => DateTime.Compare(x.EndTime,DateTime.UtcNow) > 0 && x.OrderStatus == status).ToList().Count(); ;
            var response = _order.GetAllReadOnly().OrderByDescending(x => x.OrderId).Where(x => DateTime.Compare(x.EndTime, DateTime.UtcNow) > 0 && x.OrderStatus == status).Skip(currentPage * perPage).Take(perPage).Select(x => new OrderAllListDTO
                {
                    OrderId = x.OrderId,
                    OrderName = x.OrderNumber,
                    UserName = x.BookingName,
                    Status = x.OrderStatus,
                    TotalPrice = x.Amount
                }).ToList();          



            var OrderAllListOFVM = new OrderAllListOFVM
            {
                Contact = response,
                TotalCount = total
            };

            return new ApiResultDto(OrderAllListOFVM);
        }
        public ApiResultDto GetOrderAlreadyList(int currentPage, int perPage, int status)
        {
            int total = _order.GetAllReadOnly().Where(x => DateTime.Compare(x.EndTime, DateTime.UtcNow) < 0 && x.OrderStatus == status).ToList().Count(); ;
            var response = _order.GetAllReadOnly().OrderByDescending(x => x.OrderId).Where(x => DateTime.Compare(x.EndTime, DateTime.UtcNow) < 0 && x.OrderStatus == status).Skip(currentPage * perPage).Take(perPage).Select(x => new OrderAllListDTO
            {
                OrderId = x.OrderId,
                OrderName = x.OrderNumber,
                UserName = x.BookingName,
                Status = x.OrderStatus,
                TotalPrice = x.Amount
            }).ToList();

            var OrderAllListOFVM = new OrderAllListOFVM
            {
                Contact = response,
                TotalCount = total
            };

            return new ApiResultDto(OrderAllListOFVM);
        }
        public ApiResultDto GetOrderCancelList(int orderId)
        {
            var order = _cancel.GetAllReadOnly().First(x => x.OrderId == orderId);     

            return new ApiResultDto(order);
        }

        public ApiResultDto GetOrderDate(int orderId)
        {
            var order = _order.GetById(orderId);

            bool date = DateTime.Compare(order.EndTime, DateTime.UtcNow)<0;

            return new ApiResultDto(date);
        }


        public ApiResultDto ChangeOrderStatus(ChangeOrderStatusDTO input) 
        {
            var order = _order.GetById(input.OrderId);
            order.OrderStatus=input.Status;
            var response = new ApiResultDto();
            try
            {
                _order.Update(order);
                response.Message = "更新成功";
            }
            catch
            {
                response.Message = "更新失敗";
            }
            return response;
        }
        public ApiResultDto ChangeOrderHandleStatus(ChangeOrderStatusDTO input)
        {
            var order = _order.GetById(input.OrderId);
            order.OrderStatus = input.Status;
            var ordercancel = new OrderCancel
            {
                OrderId=input.OrderId,
                CancelDate=DateTime.UtcNow,
                CancelReason=input.CancelReason,
                RefundPersent=input.RefundPersent
            };
            var response = new ApiResultDto();
            try
            {
                _order.Update(order);
                _cancel.Add(ordercancel);
                response.Message = "更新成功";
            }
            catch
            {
                response.Message = "更新失敗";
            }
            return response;
        }
        public ApiResultDto ChangeOrderStatusClear(List<int> input)
        {
            var response = new ApiResultDto();
            try
            {
                foreach (var item in input)
                {
                    var order = _order.GetById(item);
                    order.OrderStatus=(int)OrderStatus.Complete;
                    _order.Update(order);
                }               
                response.Message = "更新成功";
            }
            catch
            {
                response.Message = "更新失敗";
            }
            return response;
        }
    }
}

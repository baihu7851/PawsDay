using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced;
using PawsDay.ViewModels.ShoppingCart.OrderPlaced.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PawsDay.Services.ShoppingCart
{
    public class OrderPlacedServices
    {
        private readonly IRepository<Order> _orderRepos;
        private readonly IRepository<OrderSchedule> _orderScheduleRepos;
        private readonly IRepository<OrderPetDetail> _orderPetDetailRepos;
        private readonly IRepository<Schedule> _scheduleRepos;

        public OrderPlacedServices(IRepository<Order> orderRepos, IRepository<OrderSchedule> orderScheduleRepos, IRepository<OrderPetDetail> orderPetDetailRepos, IRepository<Schedule> scheduleRepos)
        {
            _orderRepos = orderRepos;
            _orderScheduleRepos = orderScheduleRepos;
            _orderPetDetailRepos = orderPetDetailRepos;
            _scheduleRepos = scheduleRepos;
        }

        public OrderPlacedViewModel CreateEmptyModel()
        {
            return new OrderPlacedViewModel();
        }

        public List<OrderDetailDTO> ShowListOfPlacedOrders(List<int> orderIds)
        {
            var listOfOrders = new List<OrderDetailDTO>(); //先new出要回傳的物件

            ///使用到資料表
            var orders = _orderRepos.GetAll();
            var orderSchedules = _orderScheduleRepos.GetAll();
            var orderPetDetails = _orderPetDetailRepos.GetAll();
            var schedules = _scheduleRepos.GetAll();
            ///

            //把orderIds foreach出來，跟資料庫3張表(order, orderSchedule, orderPet)去取資料
            foreach (var orderId in orderIds)
            {
                ///主要訂單資訊找出來
                var order = orders.Where(x => x.OrderId == orderId).SingleOrDefault(); 


                ///以下畫面處理用到

                //處理日期
                var serviceDate = (from o in orders
                                  join os in orderSchedules
                                  on o.OrderId equals os.OrderId
                                  where os.OrderId == orderId
                                  select os.ServiceDate.ToString("yyyy-MM-dd")).FirstOrDefault();
                //處理時間
                var timeScheduleIds = orderSchedules.Where(x => x.OrderId == orderId).OrderBy(x=>x.Schedule).Select(x=>x.Schedule).ToList();
                var beginTimeId = timeScheduleIds.First();
                var endTimeId = timeScheduleIds.Last();
                var beginTimeStr = schedules.Where(s => s.ScheduleId == beginTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[0];
                var endTimeStr = schedules.Where(s => s.ScheduleId == endTimeId).Select(s => s.TimeDesrcipt).SingleOrDefault().Split('~')[1];

                var serviceTime = beginTimeStr + " ~ " + endTimeStr;

                //處理寵物資訊
                var listOfPets = orderPetDetails.Where(p => p.OrderId == orderId).Select(p=> new PetTitleDTO()
                {
                    PetType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((PetType)p.PetType),
                    ShapeType = EnumDisplayHelper.GetDisplayName<DisplayAttribute>((ShapeType)p.ShapeType),
                }).ToList();



                ///創建訂單
                var singleOrder = new OrderDetailDTO()
                {
                    OrderNumber = order.OrderNumber,
                    PhotoUrl = order.ProductImageUrl,
                    SitterName = order.SitterName,
                    ServiceType = order.ProductName,
                    ServiceDate = serviceDate, 
                    ServiceTime = serviceTime, 
                    ListOfPets = listOfPets, 
                    CartPrice = order.Amount
                };

                listOfOrders.Add(singleOrder);

            }

            return listOfOrders;
        }

        //public void UpdateOrderSuccessStatus(int orderId)
        //{
        //    var target = _orderRepos.Where(x => x.OrderId == orderId).SingleOrDefault();
        //    target.OrderStatus = (int)OrderStatus.Success;
        //    _orderRepos.Update(target);
        //}


        public void DeleteFailedOrders(IEnumerable<string> orderIds)
        {

            foreach (string strId in orderIds)
            {
                int id = Int32.Parse(strId);
                var orderPets = _orderPetDetailRepos.Where(x => x.OrderId == id).ToList();
                _orderPetDetailRepos.DeleteRange(orderPets);

                var orderTimes = _orderScheduleRepos.Where(x => x.OrderId == id).ToList();
                _orderScheduleRepos.DeleteRange(orderTimes);

                var order = _orderRepos.Where(x => x.OrderId == id).SingleOrDefault();
                _orderRepos.Delete(order);
            }


        }


        public List<string> GetOrderNumbers(IEnumerable<string> data)
        {
            List<string> orderNumbers = new List<string>();
            foreach (var strId in data)
            {
                int intOrderId = int.Parse(strId);
                var orderNum = _orderRepos.Where(x => x.OrderId == intOrderId).SingleOrDefault().OrderNumber;
                orderNumbers.Add(orderNum);

            }

            return orderNumbers;

        }

    }
}

using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.ViewModels.MemberCenter;
using System.Collections.Generic;
using System.Linq;

namespace PawsDay.Services.MemberCenter
{
    public class MemberCenterCalenderService
    {
        private readonly IRepository<Order> _order;

        public MemberCenterCalenderService(IRepository<Order> order)
        {
            _order = order;
        }

        public ResultDto GetCalenderList(int userId)
        {
            //把全部的日期傳出去
            //訂單編號、日期、完整時間、服務類型、地址
            var result = new ResultDto();
            var datelist = _order.GetAllReadOnly().Where(o => o.CustomerId == userId).Select(o => new CalenderDto
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
    }
}

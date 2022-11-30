using PawsDay.ViewModels.SitterCenter.DetailData;
using System.Collections.Generic;
using System;
using ApplicationCore.Common;
using PawsDay.Models.MemberCenter;
using PawsDay.ViewModels.SitterCenter;

namespace PawsDay.Models.SitterCenter
{
    public class SitterOrderDetailDto
    {
        public int OrderID { get; set; } //SQL訂單編號
        public string OrderNumber { get; set; } //公開訂單號
        public int SitterID { get; set; }
        public int CustomerID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public string InvoiceNumber { get; set; }

        public DateTime ServiceTime { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public string ServiceTypeIntro { get; set; } //官方服務介紹

        public string SitterName { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductIntro { get; set; }
        public string ServiceType { get; set; }

        public string BookingName { get; set; }
        public string BookingPhone { get; set; }
        public string BookingEmail { get; set; }
        public string Addredss { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }



        /// (寵物資料) List<PetInfo> 
        public IEnumerable<SitterOrderPetDto> PetDetails { get; set; }
        public SitterOrderCancelDto CancelDetail { get; set; }
        public SitterOrderSiderBarViewModel SideBar { get; set; }

    }
}

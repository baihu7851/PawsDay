using ApplicationCore.Entities;
using PawsDay.ViewModels.ShoppingCart;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterOrderDetailViewModel
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }

        public int OrderID { get; set; } //SQL訂單編號
        public string OrderNumber { get; set; } //公開訂單號
        public int SitterID { get; set; }
        public int CustomerID { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; set; } 
        public string InvoiceNumber { get; set; }

        public string ServiceTime { get; set; }
        public DateTime BeginTime { get; set; }

        public string ServiceTypeIntro { get; set; } //官方服務介紹

        /// <summary>
        /// 顧客資訊包一起: 
        /// (訂購資料) BookingName、BookingPhone、BookingEmail、Addredss 
        /// (托育資料) Name、Phone 
        /// </summary>
        public OrderCustomerDetailData CustomerDetail { get; set; }

        /// (寵物資料) List<PetInfo> 
        public IEnumerable<PetInfoData> PetDetails { get; set; }

        /// <summary>
        /// 商品資訊包一起
        /// SitterName、ServiceType、ProductImageUrl、ProductIntro
        /// </summary>

        public OrderProductDetailData ProductDetail { get; set; }

        public OrderCancelData CancelData { get; set; }

        public SitterOrderSiderBarViewModel SideBar { get; set; }
    }
}

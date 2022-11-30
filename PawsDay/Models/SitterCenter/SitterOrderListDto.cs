using ApplicationCore.Common;
using System;

namespace PawsDay.Models.SitterCenter
{
    public class SitterOrderListDto
    {
        public int OrderID { get; set; } //SQL訂單編號
        public string OrderNumber { get; set; } //公開訂單號
        public int SitterID { get; set; }
        public int CustomerID { get; set; }
        public DateTime ServiceDate { get; set; }
        public OrderStatus PaymentStatus { get; set; }
        public string SitterName { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

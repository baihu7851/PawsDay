using ApplicationCore.Common;
using ApplicationCore.Entities;
using System;

namespace PawsDay.ViewModels.MemberCenter
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderStatus { get; set; }
        public string SitterName { get; set; }    
        public int SitterId { get; set; }
        public string ProductImage {get; set; }
        public string ServiceName { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ServiceDate { get; set; }
    }
}

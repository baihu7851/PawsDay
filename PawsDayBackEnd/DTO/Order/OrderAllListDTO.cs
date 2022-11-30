using System;
using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Order
{
    public class OrderAllListOFVM
    {
        public List<OrderAllListDTO> Contact { get; set; }
        public int TotalCount { get; set; }
    }
    public class OrderOFVM
    {
        public OrderAllListDTO Contact { get; set; }
        public int TotalCount { get; set; }
    }
    public class OrderAllListDTO
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }      
        public decimal TotalPrice { get; set; }
    }
    public class OrderIdDTO
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int ProductId { get; set; }
        public int SitterId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrderStatus { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public int InvoiceType { get; set; } //發票開立方式
        public string InvoiceId { get; set; } //發票號碼(可null)
        public string UnoformNumber { get; set; } //統一編號(可null)
        public string CompanyName { get; set; } //公司抬頭(可null)

        public string BookingName { get; set; }
        public string BookingPhone { get; set; }
        public string BookingEmail { get; set; }

        public string Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string SitterName { get; set; }
        public string ProductName { get; set; }
        public string ProductIntro { get; set; }
        public string ProductImageUrl { get; set; }
    }
    public class ChangeOrderStatusDTO
    { 
        public int OrderId { get; set; }
        public int Status { get; set; }
        public decimal RefundPersent { get; set; }
        public string CancelReason { get; set; }
    }
    

}

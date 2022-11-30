using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Order : BaseEntity
    {
        public Order()
        {
            Evaluations = new HashSet<Evaluation>();
            OrderCancels = new HashSet<OrderCancel>();
            OrderPetDetails = new HashSet<OrderPetDetail>();
            OrderSchedules = new HashSet<OrderSchedule>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public virtual Member Customer { get; set; }
        public virtual InvoiceType InvoiceTypeNavigation { get; set; }
        public virtual Member Sitter { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<OrderCancel> OrderCancels { get; set; }
        public virtual ICollection<OrderPetDetail> OrderPetDetails { get; set; }
        public virtual ICollection<OrderSchedule> OrderSchedules { get; set; }
        public virtual ICollection<OfficialContact> OfficialContacts { get; set; }
    }
}

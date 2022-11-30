using System;

namespace PawsDay.Models.MemberCenter
{
    public class OrderCancelDTO
    {
        public int OrderId { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
        public decimal Persent { get; set; }
    }
}

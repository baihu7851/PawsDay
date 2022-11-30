using System;

namespace PawsDay.Models.SitterCenter
{
    public class SitterOrderCancelDto
    {
        public int OrderId { get; set; }
        public DateTime CancelDate{ get; set; }
        public string CancelReason { get; set; }
    }
}

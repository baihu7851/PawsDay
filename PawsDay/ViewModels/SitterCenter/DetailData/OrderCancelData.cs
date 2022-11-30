using System;

namespace PawsDay.ViewModels.SitterCenter.DetailData
{
    public class OrderCancelData
    {

        public int OrderId { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
    }
}

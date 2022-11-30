using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PawsDay.ViewModels.MemberCenter
{
    public class OrderCancelViewModel
    {
        public string BackAllDate { get; set; }
        public string BackHalfDate { get; set; }
        public string BackZeroDate { get; set; }
        public decimal BackPrice { get; set; }
        public string CancelReason { get; set; }
        public List<SelectListItem> CancelReasonList { get; set; }
    }
}

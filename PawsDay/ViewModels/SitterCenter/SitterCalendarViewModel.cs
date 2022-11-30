using System.Collections;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterCalendarViewModel
    {
        public int OrderID { get; set; } //SQL訂單編號
        public string OrderNumber { get; set; } //公開訂單號
        public int ServiceMonth { get; set; }
        public int ServiceDate { get; set; }
        public string ServiceTime { get; set; }//把服務時間處理成字串
        public string ServiceType { get; set; }
    }
}

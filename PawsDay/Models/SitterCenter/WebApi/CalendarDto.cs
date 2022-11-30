using System;
using System.Web;

namespace PawsDay.Models.SitterCenter.WebApi
{
    public class CalenderDto
    {
        //訂單編號、日期、完整時間、服務類型、地址
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ServiceType { get; set; }

        public string Address { get; set; }
    }
}

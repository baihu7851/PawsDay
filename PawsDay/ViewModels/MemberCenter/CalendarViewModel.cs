using System;

namespace PawsDay.ViewModels.MemberCenter
{
    public class CalendarViewModel
    {
        public int OrderId { get; set; }
        public string SitterName { get; set; }
        public int ProductId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public string ServiceType { get; set; }

    }
}

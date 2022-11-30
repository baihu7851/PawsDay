using System;

namespace PawsDay.ViewModels.SitterCenter
{
    public class OfficialChatViewModel
    {
        public int UserId { get; set; }
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string ServiceType { get; set; }

        public string ImageUrl { get; set; }
        public string LastestContext { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsUserSpeak { get; set; }
    }
}

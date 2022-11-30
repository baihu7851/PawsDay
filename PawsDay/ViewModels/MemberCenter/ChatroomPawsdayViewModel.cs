using System;

namespace PawsDay.ViewModels.MemberCenter
{
    public class ChatroomPawsdayViewModel
    {
        public bool IsUserSpeak { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public string ServiceName { get; set; }
        public string UserImage { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
       
    }
}

using System;

namespace PawsDay.ViewModels.MemberCenter
{
    public class ChatroomSisterViewModel
    {
        public int SitterId { get; set; }
        public string SittertName { get; set; }
        public string UserImage { get; set; }
        public string Message { get; set; }
        public DateTime EditTime { get; set; }
        public bool IsMember { get; set; }
    }
}

using System;

namespace PawsDay.Models.MemberCenter
{
    public class ContactListDTO
    {
        public bool IsUserType { get; set; }
        public int UserId { get; set; }
        public string UserImage { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

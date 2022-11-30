using System;

namespace PawsDay.Models.SitterCenter
{
    public class ContactCustomerDto
    {
        public bool IsMember { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberImage { get; set; }
        public int SitterId { get; set; }
        public string SitterImage { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

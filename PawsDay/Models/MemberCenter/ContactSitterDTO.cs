using System;

namespace PawsDay.Models.MemberCenter
{
    public class ContactSitterDTO
    {
        public bool IsMember { get; set; }
        public int MemberId { get; set; }
        public string MemberImage { get; set; }
        public int SitterId { get; set; }
        public string SitterName { get; set; }
        public string SitterImage { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

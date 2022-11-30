using System;

namespace PawsDayBackEnd.DTO.Member
{
    public class MemberListDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string RegisterType { get; set; }
        public DateTime CreateTime { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Member
{
    public class MemberInfoDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
        public DateTime Birth { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public string Role { get; set; }
        

    }
}

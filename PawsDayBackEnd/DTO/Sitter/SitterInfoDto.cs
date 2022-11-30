using System;
using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Sitter
{
    public class SitterInfoDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public DateTime SubmitTime { get; set; }
        public string Experience { get; set; }

        public List<string> Answer { get; set; }
        public List<string>  DocumentUrl { get; set; }

    }
}

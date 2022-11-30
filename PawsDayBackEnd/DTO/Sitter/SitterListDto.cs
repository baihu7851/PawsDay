using System;

namespace PawsDayBackEnd.DTO.Sitter
{
    public class SitterListDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime SubmitTime { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }
}

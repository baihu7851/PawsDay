using System;
using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Message
{
    public class OrderContactDTO
    {        
        public int OrderId { get; set; }
        public string OrderNum { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool LastAnswerIsUser { get; set; }
        public List<OrderContactContentDTO> OrderContent { get; set; }        
    }
    public class OrderContactContentDTO
    {
        public int OfficialContactId { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsUserSpeak { get; set; }
    }
    public class OrderContactAnswerDTO
    { 
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public int UserType { get; set; }
    }
}

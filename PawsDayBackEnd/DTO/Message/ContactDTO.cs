using ApplicationCore.Entities;
using PawsDayBackEnd.DTO.Order;
using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Message
{
    public class ContactDTO
    {
        public int ContactId { get; set; }
        public string ContactAnswer { get; set; }
    }
    public class ContactVM
    {
        public List<Contact> Contact { get; set; }
        public int TotalCount { get; set; }
    }
    public class ContactOFVM
    {
        public List<OrderContactDTO> Contact { get; set; }
        public int TotalCount { get; set; }
    }
}

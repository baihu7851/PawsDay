using System;

namespace PawsDay.ViewModels.SitterCenter
{
    public class CustomerChatViewModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ImageUrl { get; set; }
        public string LastestContext { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public bool IsMember { get; set; }

    }
}

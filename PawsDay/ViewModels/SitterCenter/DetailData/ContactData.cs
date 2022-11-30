using System;

namespace PawsDay.ViewModels.SitterCenter.DetailData
{
    public class ContactData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public DateTimeOffset CreateTime { get; set; }

    }
}

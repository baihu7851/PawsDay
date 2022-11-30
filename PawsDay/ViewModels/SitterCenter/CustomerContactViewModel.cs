using System;
using System.Collections.Generic;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter;
using PawsDay.ViewModels.SitterCenter.DetailData;

namespace PawsDay.ViewModels.SitterCenter
{
    public class CustomerContactViewModel
    {
        public int CustomerId { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
        public string CustomerNamer { get; set; }
        public List<SitterContactDto> Contact { get; set; }
    }
}

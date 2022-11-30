using System;
using System.Collections.Generic;
using PawsDay.Models.MemberCenter;
using PawsDay.Models.SitterCenter;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.SitterCenter.DetailData;

namespace PawsDay.ViewModels.SitterCenter
{
    public class OfficialContactViewModel
    {

        public int OrderID { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
        public string InputImage { get; set; }
        public string OrderNum { get; set; }
        public List<SitterContactDto> Contact { get; set; }
        public SitterOrderSiderBarViewModel SideBar { get; set; }


    }
}

using ApplicationCore.Entities;
using PawsDay.Models.MemberCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawsDay.ViewModels.MemberCenter
{
    public class OrderContactViewModel
    {
        public int OrderID { get; set; }    
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }      
        public string InputImage { get; set; }
        public string OrderNum { get; set; }
        public List<ContactListDTO> Contact { get; set; }
        public MemberCenterOrderSidebarViewModel memberCenterOrderSidebarViewModel { get; set; }
    }
    public class OrderContactDTO
    {
        public int OrderID { get; set; }
        public string Message { get; set; }
    }
}

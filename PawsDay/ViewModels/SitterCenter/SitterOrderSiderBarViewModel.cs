using ApplicationCore.Entities;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterOrderSiderBarViewModel
    {
        public int OrderID { get; set; }
        public string OrderNumber { get; set; }
        public string SitterName { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime BegineDate { get; set; }

        public string ServiceTime { get; set; }
        public string ServiceType { get; set; }


        public List<OrderSidebarPetData> OrderSidebarPetData { get; set; }

        public decimal TotolPrice { get; set; }

        
    }
}

using ApplicationCore.Common;
using PawsDay.Models.MemberCenter;
using System;
using System.Collections.Generic;

namespace PawsDay.ViewModels.MemberCenter
{
    public class MemberCenterOrderSidebarViewModel
    {
        public int OrderId { get; set; }
        public int SitterId { get; set; }
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string SitterName { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceName { get; set; }
        public List<OrderPetLListDTO> OrderPetLListDTO { get; set; }       
        public decimal TotolPrice { get; set; }
        public decimal CancelPrice { set; get; }
        public decimal ReturnPrice { set; get; }
        public int OrderStatus { get; set; }
        public string ServiceTime { get; set; }
    }
}

using ApplicationCore.Common;
using ApplicationCore.Entities;
using PawsDay.Models.MemberCenter;
using PawsDay.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawsDay.ViewModels.MemberCenter
{
    public class OrderDetailViewModel
    {
        public string OrderNum { get; set; }
        public string MemberName { get; set; }        
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public string ProductIntro { get; set; }
        public string ServiceIntro { get; set; }
        public string ServiceName { get; set; }
        public string Address { get; set; }
        public List<OrderPetList> OrderPetList { get; set; }
        public string ConnectionName { get; set; }
        public string ConnectionPhone { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrderStatus { get; set; }
        public DateTime ServiceTime { get; set; }
        public OrderCancelDTO orderCancelDTO { get; set; }
        public MemberCenterOrderSidebarViewModel memberCenterOrderSidebarViewModel { get; set; }
        public OrderCancelViewModel orderCancelViewModel { get; set; }
        public string InvoiceID { get; set; }
    }
    public class OrderPetList
    { 
        public int OrderPetId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string ShapeType { get; set; }
        public string Gender { get; set; }
        public string Discription { get; set; }
        public int BirthYear { get; set; }
        public string Ligation { get; set; }
        public string Vaccine { get; set; }
        public string PetText { get; set; }

       
    }
}

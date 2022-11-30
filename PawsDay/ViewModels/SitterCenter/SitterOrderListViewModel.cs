using ApplicationCore.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterOrderListViewModel
    {
        public int OrderID { get; set; } //SQL訂單編號
        public string OrderNumber { get; set; } //公開訂單號
        public int SitterID { get; set; }
        public int CustomerID { get; set; }
        public DateTime ServiceDay { get; set; }
        public string ServiceMonth { get; set; }
        public int ServiceDate { get; set; }
        public string OrderStatus { get; set; }
        public string SitterName { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public string TotalPrice { get; set; }
    }
}

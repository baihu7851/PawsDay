using ApplicationCore.Entities;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SitterCenter
{
    public class ProductAdvertiseViewModel
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string ProductImage { get; set; }
        public IEnumerable<CountyandDistrictData> ServiceArea { get; set; }
        public DateTimeOffset BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

using ApplicationCore.Entities;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SitterCenter
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string Introduce { get; set; }
        public string ProductImage { get; set; }
        public IEnumerable<CountyandDistrictData> ServiceArea { get; set; }

    }
}

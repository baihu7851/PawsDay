using PawsDay.ViewModels.SitterCenter;
using System.Collections.Generic;

namespace PawsDay.Models.SitterCenter
{
    public class ProductDicountDto
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string MainImage { get; set; }
        public List<CountyandDistrictDto> ServiceArea { get; set; }

        public int Quantity { get; set; }
        public decimal Discount { get; set; }

    }
}

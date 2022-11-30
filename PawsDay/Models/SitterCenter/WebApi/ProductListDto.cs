using PawsDay.ViewModels.SitterCenter.DetailData;
using System.Collections.Generic;

namespace PawsDay.Models.SitterCenter.WebApi
{
    public class ProductListDto
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string Introduce { get; set; }
        public string ProductImage { get; set; }
        public List<ProductAreaNameDto> ServiceArea { get; set; }
    }
}

using PawsDay.ViewModels.SitterCenter;
using System.Collections.Generic;

namespace PawsDay.Models.SitterCenter
{
    public class ProductIncludeAreaDto
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string Introduce { get; set; }
        public string MainImage { get; set; }
        public List<ProductAreaNameDto> ServiceArea { get; set; }

    }
}

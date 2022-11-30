using PawsDay.ViewModels.SitterCenter;
using System;
using System.Collections.Generic;

namespace PawsDay.Models.SitterCenter
{
    public class ProductAdprojectDto
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string MainImage { get; set; }
        public List<ProductAreaNameDto> ServiceArea { get; set; }

        public DateTimeOffset BeginDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

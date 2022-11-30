using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System.Collections.Generic;
using System;
using ApplicationCore.Common;

namespace PawsDay.Models.SitterCenter
{
    public class ProductDetailDto
    {
        public int ProductID { get; set; }
        public int ServiceType { get; set; } 
        public string Introduce { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public string SitterName { get; set; }
    }
}

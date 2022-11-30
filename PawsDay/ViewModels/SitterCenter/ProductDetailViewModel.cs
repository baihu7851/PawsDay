using ApplicationCore.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Models.SitterCenter;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.SitterCenter
{
    public class ProductDetailViewModel
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public int? ProductID { get; set; }
        public string SitterName { get; set; }
        public int ServiceType { get; set; } 
        public string Introduce { get; set; }
        public string UpdateTime { get; set; }

        public List<SelectListItem> ServiceTypeList { get; set; } //服務選項
        public List<CountyDto> CountyList { get; set; } //縣市選項
        public List<CountyandDistrictDto> DistrictList { get; set; } //區域選項

    }
}

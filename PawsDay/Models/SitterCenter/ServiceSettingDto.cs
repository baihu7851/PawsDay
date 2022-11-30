using ApplicationCore.Entities;
using PawsDay.Models.SitterCenter.WebApi;
using System.Collections.Generic;

namespace PawsDay.Models.SitterCenter
{
    public class ServiceSettingDto
    {
        public int Id { get; set; }

        //基本資料(GET、POST)
        public ProductBasicDto BasicInfo { get; set; }

        //縣市(GET)
        public List<int> County { get; set; }
        //區域(GET、POST)
        public List<int> District { get; set; }

        //種類及價格(GET、POST)
        public List<ProductDetailWithPriceDto> PriceDetail { get; set; }

        //時間(GET、POST)
        public List<ServiceTimeDto> Time { get; set; }

        //照片(GET、POST)
        public List<string> ImageUrl { get; set; }

    }
}

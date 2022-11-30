using ApplicationCore.Entities;
using PawsDay.ViewModels.SitterCenter.DetailData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.SitterCenter
{
    public class ProductSalesViewModel
    {
        public int ProductID { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string ProductImage { get; set; }
        public IEnumerable<CountyandDistrictData> ServiceArea { get; set; }
        [Required(ErrorMessage = "請輸入商品數量")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "請輸入折扣(1~9折")]
        [Range(1, 9)]
        public decimal Discount { get; set; }
    }
}

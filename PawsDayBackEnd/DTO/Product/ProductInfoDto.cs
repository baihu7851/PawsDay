using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;

namespace PawsDayBackEnd.DTO.Product
{
    public class ProductInfoDto
    {
        public int ProductId { get; set; }
        public string Status { get; set; }
        public string SitterName { get; set; }
        public string SitterInfo { get; set; }
        public string ServiceType { get; set; }
        public string ProductInfo { get; set; }
        public List<string> ImageUrl { get; set; }
        public int DiscountQuantity { get; set; }
        public decimal Discount { get; set; }

        public List<PriceInfoDto> Price { get; set; }
    }
   
}

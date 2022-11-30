using System;

namespace PawsDayBackEnd.DTO.Product
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public int SitterId { get; set; }
        public string Service { get; set; }
        public DateTime CreateTime { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }

    }
}

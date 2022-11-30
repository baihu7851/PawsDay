using System.Collections.Generic;

namespace PawsDay.ViewModels.Product
{
	public class ProductDto
	{
        public int ProductId { get; set; }
        public int State { get; set; }
        public List<string> Images { get; set; }
        public int SitterId { get; set; }
        public string SitterName { get; set; }
        public string SitterImg { get; set; }
        public string SitterInfo { get; set; }
        public string ServiceType { get; set; }
        public string NoticeIntroduce { get; set; }
        public List<AreaDto> ServiceArea { get; set; }
        public string Introduce { get; set; }
        public decimal Price { get; set; }
        public List<decimal> Prices { get; set; }
        public double EvaluationAverage { get; set; }
        public int OrderQuantity { get; set; }
        public bool Collect { get; set; }
        public bool IsDelete { get; set; }
    }
}

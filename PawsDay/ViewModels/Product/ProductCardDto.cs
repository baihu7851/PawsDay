
using System.Collections.Generic;

namespace PawsDay.ViewModels.Product
{
    public class ProductCardDto
    {
        public int ProductId { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string Image { get; set; }
        public string Introduce { get; set; }
        public double EvaluationAverage { get; set; }
        public int EvaluationQuantity { get; set; }
        public int OrderQuantity { get; set; }
        public bool Collect { get; set; }
        public decimal Price { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public List<AreaDto> ServiceArea { get; set; }

        public string FilterPetTypes { get; set; }
        public string FilterDays { get; set; }
        public string FilterTimes { get; set; }
    }

}

using System.Collections;
using System.Collections.Generic;

namespace PawsDay.ViewModels.Product
{
    public class SearchCardModel
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
        public string Price { get; set; }
        public IEnumerable<AreaDto> ServiceArea { get; set; }
    }
}

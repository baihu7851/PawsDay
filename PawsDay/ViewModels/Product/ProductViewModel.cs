using ApplicationCore.Entities;
using System.Collections.Generic;
using System;

namespace PawsDay.ViewModels.Product
{
    public class ProductViewModel
    {
        public ProductModel ProductDetail { get; set; }
        public ProductDiscountDto DiscountDetail { get; set; }
        public ProductChooseDto ChooseDetail { get; set; }
        public List<ProductEvaluationDto> EvaluationDetail { get; set; }

    }
}

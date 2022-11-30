using PawsDay.ViewModels.Product;
using System;
using System.Collections.Generic;

namespace PawsDay.ViewModels.SearchProduct
{
    public class SearchProductViewModel
    {
        public string SearchInput { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Service { get; set; }
        public string Pet { get; set; }
        public List<SearchCardModel> ProductCard { get; set; }
    }
}

using ApplicationCore.Entities;
using System.Collections.Generic;
using System;

namespace PawsDay.ViewModels.MemberCenter
{
    public class CollectViewModel
    {
        public int CollectId { get; set; }
        public int ProductId { get; set; }
        public string ProductImage { get; set; }
        public string SitterName { get; set; }
        public string TypeName { get; set; }        
        public string County { get; set; }
        public int OrderCount { get; set; }
        public double EvaluationAverage { get; set; }
        public int EvaluationCount { get; set; }
        public decimal Price { get; set; }
        public bool IsCollect { get; set; }
    }
}

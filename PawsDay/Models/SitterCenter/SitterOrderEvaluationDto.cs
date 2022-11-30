using PawsDay.ViewModels.SitterCenter;
using System;

namespace PawsDay.Models.SitterCenter
{
    public class SitterOrderEvaluationDto
    {
        public int Orderid { get; set; }
        public string OrderNumber { get; set; }
        public string UserImageUrl { get; set; }
        public int EvaluationScore { get; set; }
        public string EvaluationMessage { get; set; }
        public DateTime CreateTime { get; set; }

        public SitterOrderSiderBarViewModel Sidebar { get; set; }
    }
}

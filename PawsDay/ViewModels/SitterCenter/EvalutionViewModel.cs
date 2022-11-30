using System;

namespace PawsDay.ViewModels.SitterCenter
{
    public class EvalutionViewModel
    {
        //訂單明細與歷史評論可以共用

        public bool IsValid { get; set; }
        public string Message { get; set; }
        public int Orderid { get; set; }
        public string OrderNumber { get; set;}
        public string UserImageUrl { get; set; }
        public int EvaluationScore { get; set; }
        public string EvaluationMessage { get; set; }
        public DateTime CreateTime { get; set; }

        public SitterOrderSiderBarViewModel SideBar { get; set; }
    }
}

using ApplicationCore.Entities;
using System;

namespace PawsDay.ViewModels.MemberCenter
{
    public class HistoryEvaluationViewModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int Evaluation { get; set; }
        public int UserId { get; set; }
        public string UserImage { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }

    }
}

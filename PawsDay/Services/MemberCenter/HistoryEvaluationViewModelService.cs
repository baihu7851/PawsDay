using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDay.ViewModels.MemberCenter;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PawsDay.Services.MemberCenter
{
    public class HistoryEvaluationViewModelService
    {
        private readonly IRepository<Order> _order;
        private readonly IRepository<Evaluation> _evaluation;
        private readonly IRepository<RegisterSitter> _sister;

        public HistoryEvaluationViewModelService(IRepository<Order> order, IRepository<Evaluation> evaluation, IRepository<RegisterSitter> sister)
        {
            _order = order;
            _evaluation = evaluation;
            _sister = sister;
        }

        public IEnumerable<HistoryEvaluationViewModel> GetHistoryEvaluations(int userId)
        {
            var evaluations = from e in _evaluation.GetAllReadOnly()
                              join o in _order.GetAllReadOnly() on e.OrderId equals o.OrderId
                              join s in _sister.GetAllReadOnly() on o.SitterId equals s.MemberId
                              where e.OrderId== o.OrderId 
                              && e.UserType==2 && o.CustomerId==userId
                              orderby e.CreateTime descending
                              select new HistoryEvaluationViewModel()
                              { OrderId=o.OrderId,
                                OrderNumber=o.OrderNumber,
                                UserImage=s.SitterPicture,
                                Evaluation=e.EvaluationScore,
                                Message=e.Message,
                                CreateTime=e.CreateTime };

            var evaluation = evaluations.ToList();

            return evaluation;
        }

     

    }
}

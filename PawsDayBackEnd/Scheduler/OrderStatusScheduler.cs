using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Scheduler
{
    public class OrderStatusScheduler : IInvocable
    {
        private readonly IRepository<Order> _order;        
        private readonly ILogger<OrderStatusScheduler> _logger;

        public OrderStatusScheduler(IRepository<Order> order, ILogger<OrderStatusScheduler> logger)
        {
            _order = order;           
            _logger = logger;
        }

        public Task Invoke()
        {           
            OrderStatusToComplete();
            return Task.CompletedTask;
        }

        private void OrderStatusToComplete()
        {
            var order = _order.GetAllReadOnly().Where(x => x.OrderStatus == (int)OrderStatus.Success).ToList();
            var orderStatus = order.Where(x=> (DateTime.UtcNow - x.EndTime).Days > 10).ToList();
            try
            {
                if (!orderStatus.Any())
                {
                    return;
                }
                foreach (var item in orderStatus)
                {                   
                    item.OrderStatus = (int)OrderStatus.Complete;
                    _order.Update(item);
                    
                }
                _logger.LogInformation("OrderStatusToComplete is Complete");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

        }

    }
}

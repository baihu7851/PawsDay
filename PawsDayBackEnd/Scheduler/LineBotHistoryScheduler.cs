using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Scheduler
{
    public class LineBotHistoryScheduler: IInvocable
    {
        public readonly IRepository<LineBotHistory> _history;
        private readonly ILogger<BlockTokenScheduler> _logger;

        public LineBotHistoryScheduler(IRepository<LineBotHistory> history, ILogger<BlockTokenScheduler> logger)
        {
            _history = history;
            _logger = logger;
        }

        public Task Invoke()
        {
            RemoveExpiredHistory();
            Console.WriteLine("刪除過期line對話紀錄");
            return Task.CompletedTask;
        }

        public void RemoveExpiredHistory()
        {
            var history = _history.GetAll().Where(h => h.CreateTime < DateTimeOffset.UtcNow.AddMinutes(5)).ToList();

            if (!history.Any())
            {
                return;
            }

            try
            {
                _history.DeleteRange(history);
                _logger.LogInformation("Remove history success!");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

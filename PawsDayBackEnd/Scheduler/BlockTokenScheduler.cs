using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Scheduler
{
    public class BlockTokenScheduler : IInvocable
    {
        private readonly IRepository<BlockToken> _token;
        private readonly ILogger<BlockTokenScheduler> _logger;

        public BlockTokenScheduler(IRepository<BlockToken> token, ILogger<BlockTokenScheduler> logger)
        {
            _token = token;
            _logger = logger;
        }

        public Task Invoke()
        {
            RemoveExpiredToken();
            Console.WriteLine("刪除黑名單");
            return Task.CompletedTask;
        }
        private void RemoveExpiredToken()
        {
            var tokens = _token.GetAllReadOnly().Where(x=>x.ExpireTime<DateTimeOffset.UtcNow).ToList();
            try
            {
                if (!tokens.Any())
                {
                    return;
                }

                _token.DeleteRange(tokens);
                _logger.LogInformation("Remove blockTokens success!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }

}

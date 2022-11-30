using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PawsDay.Interfaces.Account;

namespace PawsDay.Services.Account
{
    public class LineLoginService : ILineLoginService 
    {
        private IConfigurationSection _channel_ID, _channel_Secret, _callBackURLConfig;
        private readonly HttpContext _httpContext;  
        private string _callBackURL;



        public LineLoginService(IConfiguration config,IHttpContextAccessor httpContextAccessor)
        {
            _channel_ID = config.GetSection("LINE-Login-Setting:channel_ID");
            _channel_Secret = config.GetSection("LINE-Login-Setting:channel_Secret");
            _callBackURLConfig = config.GetSection("LINE-Login-Setting:callBackURL");
            _httpContext = httpContextAccessor.HttpContext;
            _callBackURL = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}{_callBackURLConfig.Value}";
        }

        public IConfigurationSection Channel_ID { get => _channel_ID; set => _channel_ID = value; }
        public IConfigurationSection Channel_Secret { get => _channel_Secret; set => _channel_Secret = value; }
        public string CallBackURL { get => _callBackURL; set => _callBackURL = value; }
               
    }


}

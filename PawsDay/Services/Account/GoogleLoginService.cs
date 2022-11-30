using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PawsDay.Interfaces.Account;
using System;

namespace PawsDay.Services.Account
{
    public class GoogleLoginService : IGoogleLoginService
    {

        private IConfigurationSection _client_ID, _client_Secret, _callBackURLConfig;
        private readonly HttpContext _httpContext;
        private string _callBackURL;

        public GoogleLoginService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _client_ID = config.GetSection("Google-Login-Setting:client_id");
            _client_Secret = config.GetSection("Google-Login-Setting:client_secret");
            _callBackURLConfig = config.GetSection("Google-Login-Setting:call_back_url");
            _httpContext = httpContextAccessor.HttpContext;           
            _callBackURL = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}{_callBackURLConfig.Value}";
        }

        public IConfigurationSection Client_ID { get => _client_ID; set => _client_ID = value; }
        public IConfigurationSection Client_Secret { get => _client_Secret; set => _client_Secret = value; }
        public string CallBackURL { get => _callBackURL; set => _callBackURL = value; }
    }
}

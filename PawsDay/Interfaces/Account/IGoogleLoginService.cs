using Microsoft.Extensions.Configuration;

namespace PawsDay.Interfaces.Account
{
    public interface IGoogleLoginService
    {
        IConfigurationSection Client_ID { get; set; }
        IConfigurationSection Client_Secret { get; set; }
        string CallBackURL { get; set; }
    }
}
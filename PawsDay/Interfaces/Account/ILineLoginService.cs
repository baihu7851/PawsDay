using Microsoft.Extensions.Configuration;

namespace PawsDay.Interfaces.Account
{
    public interface ILineLoginService
    {
        IConfigurationSection Channel_ID { get; set; }
        IConfigurationSection Channel_Secret { get; set; }
        string CallBackURL { get; set; }


    }
}

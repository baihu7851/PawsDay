using System;

namespace PawsDay.ViewModels.ShoppingCart.Booking
{
    public class SendNewebPayDTO
    {
        public string MerchantID { get; set; }
        public string Version { get; set; }
        public string TradeInfo { get; set; }
        public string TradeSha { get; set; }
    }

    public class BuildNewebPayInfo
    {
        public BuildNewebPayInfo()
        {
            HashKey = "lAR3oEkjrietnohH4wHSgYIwnPcOO5BI";
            HashIV = "C3lq4X3LZgKCVWbP";
            MerchantID = "MS144852133";
            //必填
            RespondType = "JSON";
            Version = "2.0";
            TimeStamp = ((int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
            
            
        }

        public string HashKey { get; set; } //
        public string HashIV { get; set; } //
        public string ChannelID { get; set; }
        public string RespondType { get; set; }//QQ
        public string MerchantID { get; set; } //QQ
        public string TimeStamp { get; set; } //QQ
        public string Version { get; set; }//QQ
        public string MerchantOrderNo { get; set; } //QQ
        public string ItemDesc { get; set; }//QQ
        public int Amt { get; set; }//QQ
        public string ExpireDate { get; set; }
        public string ReturnURL { get; set; }
        public string CustomerURL { get; set; }
        public string NotifyURL { get; set; }
        public string ClientBackURL { get; set; }
        public string Email { get; set; }
    }

    
}

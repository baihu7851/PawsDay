using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace PawsDay.ViewModels.ShoppingCart.Booking
{
    public class SendECPayDTO
    {
        public SendECPayDTO()
        {
            
            HashKey = "pwFHCqoQZGmho4w6";
            HashIV = "EkRm7iFT261dpevs";
            MerchantID = "3002607";
            IgnorePayment = "ATM#ApplePay#BARCODE#CVS#WebATM";
            PaymentType = "aio";
            MerchantTradeDate = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");
            //MerchantTradeDate = "2013/03/12 15:30:23";
            ChoosePayment = "Credit";
            EncryptType = 1;
            TradeDesc = "PAWS DAY商城購物";
        }

        //界接服務參數begin
        
        public string HashKey { get; set; }//建構式
        public string HashIV { get; set; }//建構式
        public string MerchantID { get; set; }//建構式
        //界接服務參數end

        //基本參數begin
        public string ReturnURL { get; set; } //Controller/Action接收回傳
        public string PaymentType { get; set; }//建構式
        public string OrderResultURL { get; set; } //跳回自己頁面的網址
        public string ClientBackURL { get; set; }
        public string CustomField1 { get; set; }//客製化訊息-orderId
        public string CustomField2 { get; set; }//客製化訊息-cartId


        public string ChoosePayment { get; set; }//建構式

        public string IgnorePayment { get; set; }//建構式
        public int EncryptType { get; set; }//建構式
        //基本參數end

        //產品參數begin
        public string ItemName { get; set; } //商品品項  
        public string MerchantTradeNo { get; set; }//商品訂單編號
        public string MerchantTradeDate { get; set; }//商品下單日期 //建構式
        public int TotalAmount { get; set; }//商品總價
        public string TradeDesc { get; set; }//商城描述 //建構式
        //產品參數end


        //檢查碼，將以上資訊都包裝起來加密
        public string CheckMacValue { get; set; } 
    }
}

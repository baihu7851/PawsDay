using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PawsDay.Services.SendGridServices.DTO;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PawsDay.Services.SendGridServices
{
    public class SendGridService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apikey;
        private readonly HttpContext _httpContext;

        public SendGridService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _apikey = _configuration.GetSection("SendGrid:api_secret").Value;
            _httpContext = httpContextAccessor.HttpContext;
        }
        private EmailALLDTO GetEmailApi(EmailContentDTO input)
        {
            var apiKey = _apikey;
            var email = new EmailALLDTO
            {
                Client= new SendGridClient(apiKey),
                PawsDay= new EmailAddress("pawsday888@gmail.com", "PawsDay"),
                PlainTextContent = "",
                ToUser = new EmailAddress(input.UserEmail, input.UserName)
        };
            return email;
        }
        private async Task<Response> SendEmail(EmailALLDTO email)
        {
            var msg = MailHelper.CreateSingleEmail(email.PawsDay, email.ToUser, email.EmailTitle, email.PlainTextContent, email.EmailContent);
            return await email.Client.SendEmailAsync(msg);
        }
        public async Task CreateOrderEmailToMember(EmailOrderDTO input) 
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle ="您的訂單已付款成功";                      
            email.EmailContent = $"<div\r\n        style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂購成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.ContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    感謝您的訂購，您的訂單已確認付款成功！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            保姆名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.SitterName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n                    </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/MemberCenter/OrderDetail/{input.OrderNum}\">\r\n                        至網頁查看訂單明細\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    1.若有特殊需求或是疾病毛孩，請先與保姆溝通<br>\r\n                    2.訂單如需取消，請詳閱取消政策\r\n                </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response= await SendEmail(email);

        }
        public async Task CreateOrderEmailToSitter(EmailOrderDTO input)
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle = "您的服務已被預訂";
            email.EmailContent = $"<div\r\n        style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂購成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.SitterName} 保姆您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    您的服務已被預訂！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            聯絡人名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.MemberName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n                    </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/SitterCenter/OrderDetail/{input.OrderNum}\">\r\n                        至網頁查看訂單明細\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    1.若有訂單相關問題，請先與主人溝通<br>\r\n 2.訂單如需取消，請詳閱取消政策<br>\r\n 3.服務時間三天前，將無法取消訂單<br>\r\n </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);

        }
        public async Task MemberCancelOrderEmailToMember(EmailOrderDTO input)
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle = "您的訂單已取消";
            email.EmailContent = $"<div\r\n  style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂單取消成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.ContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    您的訂單已取消！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            保姆名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n     {input.SitterName}\r\n </span><br>\r\n  <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n            <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n                            取消原因\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelReason}\r\n    </span><br>     <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n                            退款金額\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelBackAmount}\r\n                        </span><br><span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block; color: #dc3545;\">\r\n                            取消時間\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CreatrTime}\r\n                        </span><br>   </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/MemberCenter/OrderDetail/{input.OrderNum}\">\r\n                        至網頁查看訂單明細\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    1.訂單已取消，如有保姆需求請重新下訂<br>\r\n    </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);
        }
        public async Task MemberCancelOrderEmailToSitter(EmailOrderDTO input)
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle = "您的服務已被消費者取消";
            email.EmailContent = $"<div\r\n  style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂單取消成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.ContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    您的服務已被消費者取消！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            聯絡人名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n     {input.MemberName}\r\n </span><br>\r\n  <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n    </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n            <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n    取消原因\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelReason}\r\n    </span><br>   <span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block; color: #dc3545;\">\r\n                            取消時間\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n   {input.EmailCancelOrderDTO.CreatrTime}\r\n   </span><br>   </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/SitterCenter/OrderDetail/{input.OrderNum}\">\r\n                        至網頁查看訂單明細\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    至網頁查看訂單明細<br>\r\n    </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);
        }

        public async Task SitterCancelOrderEmailToMember(EmailOrderDTO input)
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle = "您的訂單已被保姆取消";
            email.EmailContent = $"<div\r\n  style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂單取消成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.ContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    您的訂單已被保姆取消！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            保姆名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n     {input.SitterName}\r\n </span><br>\r\n  <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n            <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n                            取消原因\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelReason}\r\n    </span><br>     <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n                            退款金額\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelBackAmount}\r\n                        </span><br><span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block; color: #dc3545;\">\r\n                            取消時間\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CreatrTime}\r\n                        </span><br>   </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/MemberCenter/OrderDetail/{input.OrderNum}\">\r\n                        1.訂單已取消，如有保姆需求請重新下訂\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    1.訂單已取消，如有保姆需求請重新下訂<br>\r\n    </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);
        }
        public async Task SitterCancelOrderEmailToSitter(EmailOrderDTO input)
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle = "您的訂單已取消";
            email.EmailContent = $"<div\r\n  style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂單取消成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.ContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    您的訂單已取消！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            聯絡人名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n     {input.MemberName}\r\n </span><br>\r\n  <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n    </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n            <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n    取消原因\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelReason}\r\n    </span><br>   <span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block; color: #dc3545;\">\r\n                            取消時間\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n   {input.EmailCancelOrderDTO.CreatrTime}\r\n   </span><br>   </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/SitterCenter/OrderDetail/{input.OrderNum}\">\r\n                        至網頁查看訂單明細\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    1.訂單已取消，如商品有更動請修改商品，謝謝<br>\r\n    </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);
        }
        public async Task CancelOrderEmail(EmailOrderDTO input)
        {
            var email = GetEmailApi(input.ContentDTO);
            email.EmailTitle = "您的訂單已取消";
            email.EmailContent = $"<div\r\n  style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                訂單取消成功\r\n            </div>\r\n            <div style=\"margin:20px 10px;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.ContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    您的訂單已取消！\r\n                </div>\r\n                <div\r\n                    style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            保姆名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n     {input.SitterName}\r\n </span><br>\r\n  <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            訂單編號\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.OrderNum}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            服務名稱\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceName}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用日期\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceDate}\r\n                        </span><br>\r\n                        <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block;\">\r\n                            使用時間\r\n                        </span>\r\n                        <span style=\"width:70%\">\r\n                            {input.ServiceTime}\r\n                        </span><br>\r\n            <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n                            取消原因\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelReason}\r\n    </span><br>     <span style=\"width:30%; font-weight: 700; margin-bottom: 15px;display: inline-block; color: #dc3545;\">\r\n                            退款金額\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CancelBackAmount}\r\n                        </span><br><span style=\"width:30%; font-weight: 700; margin-bottom: 5px;display: inline-block; color: #dc3545;\">\r\n                            取消時間\r\n                        </span>\r\n                        <span style=\"width:70%; color: #dc3545;\">\r\n                            {input.EmailCancelOrderDTO.CreatrTime}\r\n                        </span><br>   </div>\r\n                </div>\r\n                <div style=\"text-align: center; padding-bottom: 5px;\">\r\n                    <a style=\"text-decoration: none; color:#f1c493; box-sizing: border-box; padding: 5px 20px; border: 1px solid #f1c493; border-radius: 6px;\"\r\n                        href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/MemberCenter/OrderDetail/{input.OrderNum}\">\r\n                        至網頁查看訂單明細\r\n                    </a>\r\n                </div>\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #0c0b0b; box-sizing: border-box; padding: 10px; margin-bottom: 20px;\">\r\n                <div style=\"font-size: 12px;\">\r\n                    注意事項\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-left: 30px;\">\r\n                    1.訂單已取消，如有保姆需求請重新下訂<br>\r\n    </div>\r\n            </div>\r\n            <div style=\"font-size: 12px;box-sizing: border-box; padding: 15px 10px;\">\r\n                <div style=\"margin-bottom: 10px;\">\r\n                    如有任何問題，歡迎透過 <a style=\"color:#a26d39;\" href=\"mailto:pawsday888@gmail.com\">客服信箱</a> 聯繫，我們將竭誠為您處理。\r\n                </div>\r\n                <div style=\"font-size: 10px; text-align: center;\">\r\n                    *本郵件由系統自動傳送，請勿直接回覆*\r\n                </div>\r\n            </div>\r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);
        }
        public async Task<bool> CheckRegisterEmailAsync(EmailContentDTO input)
        {
            var email = GetEmailApi(input);
            email.EmailTitle = "PawsDay 會員帳號驗證信";
            email.EmailContent = $@"<html lang=""en"">

<head>
    <title>PawsDay email member</title>
</head>

<div>
    <table
        style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:100%;background-color:#fef4dd;border-top:6px solid #a26d39;border-collapse:collapse;table-layout:fixed;"">
        <tr>
            <td align=""center"">
                <table
                    style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:600px;border-collapse:collapse;table-layout:fixed;"">
                    <tr>
                        <td style=""width:200px;padding-left:5%;"">
                            <a href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}"">
                                <img style=""float:left;width:150px;height:auto;"" alt=""pawsday""
                                    src=""https://ppt.cc/fflnAx@.png"">
                            </a>
                        </td>
                        <td
                            style=""padding-right:5%;text-align:right;color:#BDBDBD;font-size:16px;line-height:1.2;padding-top:23px;padding-bottom:23px;"">
                            PawsDay 會員中心
                        </td>
                    </tr>
                    <tr style=""width:100%;"">
                        <td style=""width:100%;"" align=""center"" colspan=""2"">
                            <table
                                style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:90%;border-collapse:collapse;table-layout:fixed;background-color:#ffffff;"">
                                <tr>
                                    <td align=""center""
                                        style=""color:#0E071E;font-size:36px;line-height:1.43;padding-top:10%;padding-bottom:7%;padding-left:8%;padding-right:8%;"">
                                        啟用帳號
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#828282;font-size:14px;line-height:1.43;padding-bottom:12px;padding-left:8%;padding-right:8%;"">
                                        請點擊以下按鈕，啟用 PawsDay 帳號。注意: 這個連結有效時間為 30 分鐘，若是連結已經逾時，請重新登入網站並重新收信。
                                    </td>
                                </tr>
                                <tr>
                                    <td align=""center"" style=""padding-bottom:50px;padding-left:8%;padding-right:8%;"">
                                        <div align=""center"" style="" "">
                                            <table
                                                style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:250px;border-collapse:collapse;table-layout:fixed;background-color:#f1c493;"">
                                                <tr style=""width:100%;"">
                                                    <td align=""center""
                                                        style=""width:100%;font-size:18px;padding-top:11px;padding-bottom:11px;"">
                                                        <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                                            href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/Register{input.RouteUrl}""
                                                            style=""display:inline-block;width:100%;line-height:1.43;text-decoration:none;color:#562f09;"">驗證</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#828282;font-size:14px;line-height:1.43;padding-bottom:12px;padding-left:8%;padding-right:8%;"">
                                        如果您沒有申請 PawsDay 帳號，卻收到此郵件，表示您的電子信箱可能不小心被輸入，請直接忽略或是刪除此郵件。您的帳號將不會被啟用。
                                    </td>
                                </tr>

                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:12px;line-height:1.43;padding-bottom:20px;padding-left:8%;padding-right:8%;"">
                                        本郵件是由系統自動寄送，請勿直接回覆此信。若是您有任何帳號問題，歡迎聯絡<a
                                            href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/ContactUs""
                                            class=""service-center"" style=""color: #a26d39;
                                    font-family: sans-serif;
                                    font-weight: 700;"">PawsDay
                                            客服中心</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style=""text-align:center;color:#BDBDBD;font-size:12px;width:100%;"">
                        <td colspan=""2"" align=""center""
                            style=""width:100%;padding-top:20px;padding-bottom:10px;padding-left:5%;padding-right:5%;"">
                            Copyright © PawsDay Corporation. All Rights Reserved.
                        </td>
                    </tr>
                    <tr style=""width:100%;"">
                        <td colspan=""2""
                            style=""width:100%;text-align:center;color:#BDBDBD;padding-bottom:40px;padding-left:5%;padding-right:5%;""
                            align=""center"">
                            <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/PrivacyPolicy""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">隱私權政策</a>
                            <span style=""padding-left:3%;padding-right:3%;"">•</span>
                            <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/UserAgreement""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">使用者條款</a>
                            <span style=""padding-left:3%;padding-right:3%;"">•</span>
                            <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/ContactUs""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">聯絡我們</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</div>

</html>";

           
            var response = await SendEmail(email);
       

            return response.IsSuccessStatusCode;
        }

        public async Task RegisterOkEmail(EmailContentDTO input)
        {
            var email = GetEmailApi(input);
            email.EmailTitle = "PawsDay 會員註冊成功";
            email.EmailContent = $@"<html lang=""en"">

<head>
    <title>PawsDay email member</title>
</head>

<div>
    <table
        style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:100%;background-color:#fef4dd;border-top:6px solid #a26d39;border-collapse:collapse;table-layout:fixed;"">
        <tr>
            <td align=""center"">
                <table
                    style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:600px;border-collapse:collapse;table-layout:fixed;"">
                    <tr>
                        <td style=""width:200px;padding-left:5%;"">
                            <a href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}"">
                                <img style=""float:left;width:150px;height:auto;"" alt=""pawsday""
                                    src=""https://ppt.cc/fflnAx@.png"">
                            </a>
                        </td>
                        <td
                            style=""padding-right:5%;text-align:right;color:#BDBDBD;font-size:16px;line-height:1.2;padding-top:23px;padding-bottom:23px;"">
                            PawsDay 會員中心
                        </td>
                    </tr>
                    <tr style=""width:100%;"">
                        <td style=""width:100%;"" align=""center"" colspan=""2"">
                            <table
                                style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:90%;border-collapse:collapse;table-layout:fixed;background-color:#ffffff;"">
                                <tr>
                                    <td align=""center""
                                        style=""color:#0E071E;font-size:36px;line-height:1.43;padding-top:10%;padding-bottom:5%;padding-left:8%;padding-right:8%;"">
                                        註冊成功
                                    </td>
                                </tr>
                                <tr>
                                    <td align=""center"" style=""padding-bottom:16px;"">
                                        <img width=""66px"" src=""https://ppt.cc/fOw5Sx@.png"" height=""66"">
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:16px;line-height:1.43;padding-bottom:2px;padding-left:8%;padding-right:8%;"">
                                        此訊息是對您的註冊請求的自動回覆。
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:16px;line-height:1.43;padding-bottom:76px;padding-left:8%;padding-right:8%;"">
                                        恭喜您成功加入 PawsDay 網站，您的帳號已被啟動。
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:12px;line-height:1.43;padding-bottom:11%;padding-left:8%;padding-right:8%;"">
                                        獻上最誠摯的祝福，
                                        <br> - PawsDay 團隊
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style=""text-align:center;color:#BDBDBD;font-size:12px;width:100%;"">
                        <td colspan=""2"" align=""center""
                            style=""width:100%;padding-top:20px;padding-bottom:10px;padding-left:5%;padding-right:5%;"">
                            Copyright © PawsDay Corporation. All Rights Reserved.
                        </td>
                    </tr>
                    <tr style=""width:100%;"">
                        <td colspan=""2""
                            style=""width:100%;text-align:center;color:#BDBDBD;padding-bottom:40px;padding-left:5%;padding-right:5%;""
                            align=""center"">
                            <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/PrivacyPolicy""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">隱私權政策</a>
                            <span style=""padding-left:3%;padding-right:3%;"">•</span>
                            <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/UserAgreement""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">使用者條款</a>
                            <span style=""padding-left:3%;padding-right:3%;"">•</span>
                            <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/ContactUs""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">聯絡我們</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</div>

</html>";
            var response = await SendEmail(email);
        }



        public async Task<bool> ForgotPasswordEmailAsync(EmailContentDTO input)
        {
            var email = GetEmailApi(input);
            email.EmailTitle = "PawsDay 電子信箱驗證";
            email.EmailContent = $@"<html lang=""en"">

<head>
    <title>PawsDay email member</title>
</head>

<div>
    <table
        style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:100%;background-color: #fef4dd;border-top:6px solid #a26d39;border-collapse:collapse;table-layout:fixed;"">
        <tr>
            <td align=""center"">
                <table
                    style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:600px;border-collapse:collapse;table-layout:fixed;"">
                    <tr>
                        <td style=""width:200px;padding-left:5%;"">
                            <a href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}"">
                                <img style=""float:left;width:150px;height:auto;"" alt=""pawsday""
                                    src=""https://ppt.cc/fflnAx@.png"">
                            </a>
                        </td>
                        <td
                            style=""padding-right:5%;text-align:right;color:#BDBDBD;font-size:16px;line-height:1.2;padding-top:23px;padding-bottom:23px;"">
                            PawsDay 會員中心
                        </td>
                    </tr>
                    <tr style=""width:100%;"">
                        <td style=""width:100%;"" align=""center"" colspan=""2"">
                            <table
                                style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:90%;border-collapse:collapse;table-layout:fixed;background-color:#ffffff;"">
                                <tr>
                                    <td align=""center""
                                        style=""color:#0E071E;font-size:36px;line-height:1.43;padding-top:10%;padding-bottom:7%;padding-left:8%;padding-right:8%;"">
                                        驗證您的新密碼
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:16px;line-height:1.43;padding-bottom:20px;padding-left:8%;padding-right:8%;"">
                                        你好 <a rel=""nofollow noopener noreferrer""
                                            style=""color: #a26d39;display:inline;text-decoration:none;cursor:default;"">{input.UserEmail}</a>
                                        ,
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:16px;line-height:1.43;padding-bottom:30px;padding-left:8%;padding-right:8%;"">
                                        要重設密碼，請點擊下面的連結：
                                    </td>
                                </tr>
                                <tr>
                                    <td align=""center"" style=""padding-bottom:50px;padding-left:8%;padding-right:8%;"">
                                        <div align=""center"" style="" "">
                                            <table
                                                style=""font-family:Gill Sans MT, sans-serif;border-spacing:0px !important;width:238px;border-collapse:collapse;table-layout:fixed;background-color:#f1c493;"">
                                                <tr style=""width:100%;"">
                                                    <td align=""center"" style=""width:100%;font-size:18px;padding-top:11px;
                                                        padding-bottom:11px;"">
                                                        <a rel=""nofollow noopener noreferrer"" target=""_blank""
                                                            href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/ForgotPassword{input.RouteUrl}""
                                                            style=""display:inline-block;
                                                            color: #562f09;width:100%;line-height:1.43;text-decoration:none;"">點擊</a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td
                                        style=""color:#828282;font-size:14px;line-height:1.43;padding-bottom:12px;padding-left:8%;padding-right:8%;"">
                                        如果您沒有申請 PawsDay 忘記帳號，卻收到此郵件，表示您的電子信箱可能不小心被輸入，請直接忽略或是刪除此郵件。您的帳號將不會修改密碼。
                                    </td>
                                </tr>

                                <tr>
                                    <td
                                        style=""color:#0E071E;font-size:14px;line-height:1.43;padding-bottom:0.5%;padding-left:8%;padding-right:8%;"">
                                        此致，
                                        <br> - PawsDay 團隊
                                    </td>
                                </tr>
                                <tr>
                                    <td 
                                        style=""color:#0E071E;font-size:12px;line-height:1.43;padding-top:10%;padding-bottom:7%;padding-left:8%;padding-right:8%;"">
                                        本郵件是由系統自動寄送，請勿直接回覆此信。若是您有任何帳號問題，歡迎聯絡 <a  class=""service-center"" style=""color: #a26d39;
                                        font-family: sans-serif;
                                        font-weight: 700;"">PawsDay
                                            客服中心</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style=""text-align:center;color:#BDBDBD;font-size:12px;width:100%;"">
                        <td colspan=""2"" align=""center""
                            style=""width:100%;padding-top:20px;padding-bottom:10px;padding-left:5%;padding-right:5%;"">
                            Copyright © PawsDay Corporation. All Rights Reserved.
                        </td>
                    </tr>
                    <tr style=""width:100%;"">
                        <td colspan=""2""
                            style=""width:100%;text-align:center;color:#BDBDBD;padding-bottom:40px;padding-left:5%;padding-right:5%;""
                            align=""center"">
                            <a rel=""nofollow noopener noreferrer"" target=""_blank"" href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/PrivacyPolicy""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">隱私權政策</a>
                            <span style=""padding-left:3%;padding-right:3%;"">•</span>
                            <a rel=""nofollow noopener noreferrer"" target=""_blank"" href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/UserAgreement""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">使用者條款</a>
                            <span style=""padding-left:3%;padding-right:3%;"">•</span>
                            <a rel=""nofollow noopener noreferrer"" target=""_blank"" href=""{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}/StaticWeb/ContactUs""
                                style=""color:#BDBDBD;text-decoration:underline;font-size:13px;"">聯絡我們</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</div>

</html>";
            var response = await SendEmail(email);

            return response.IsSuccessStatusCode;

        }


        public async Task SuccessfulChangePasswordEmail(EmailContentDTO input)
        {
            var email = GetEmailApi(input);
            email.EmailTitle = "PawsDay 會員密碼變更成功";
            email.EmailContent = $@"";
            var response = await SendEmail(email);
        }

    }
}

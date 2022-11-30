using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PawsDayBackEnd.Services.SendGrid.DTO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;



namespace PawsDayBackEnd.Services.SendGrid
{
    public class SendGridServices
    {
        private readonly IConfiguration _configuration;
        private readonly string _apikey;
        private readonly HttpContext _httpContext;

        public SendGridServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
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
                Client = new SendGridClient(apiKey),
                PawsDay = new EmailAddress("pawsday888@gmail.com", "PawsDay"),
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

        public async Task AnswerContactUs(EmailContactUsDTO input)
        {
            var email = GetEmailApi(input.EmailContentDTO);
            email.EmailTitle = "PawsDay 已回覆您的來信";
            email.EmailContent = $"<div\r\n        style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">\r\n        <div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">\r\n            <div style=\"margin: auto; width:150px; background-color: #fff;\">\r\n                <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n            </div>\r\n            <div\r\n                style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">\r\n                聯絡我們\r\n            </div>\r\n            <div style=\"margin:20px 10px; background-color: #fff;\">\r\n                <div style=\"font-size: 14px; margin-bottom: 10px;\">\r\n                    親愛的 {input.EmailContentDTO.UserName} 您好：\r\n                </div>\r\n                <div style=\"font-size: 12px; margin-bottom: 10px;\">\r\n                    已收到您的來信\r\n                </div>\r\n                <div style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <span style=\"width:20%; font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            主旨：\r\n                        </span>\r\n                        <span style=\"width:80%\">\r\n                            {input.ContactTitle}\r\n                        </span><br>\r\n                        <div style=\"font-weight: 700; margin-bottom: 15px; display: inline-block;\">\r\n                            您的意見：\r\n                        </div>\r\n                        <div style=\"margin-left: 50px;\">\r\n                            {input.ContactContent}\r\n                        </div><br>                        \r\n                    </div>\r\n                </div>\r\n                <div style=\"background-color: #f5f5f5; box-sizing: border-box; padding: 10px 10px;font-size: 12px; margin-bottom: 20px;\">\r\n                    <div style=>\r\n                        <div style=\"font-weight: 700; margin-bottom: 5px; display: inline-block;\">\r\n                            回覆：\r\n                        </div>\r\n                        <div style=\" margin-left: 50px;\">\r\n                           {input.AnswerContent}\r\n                        </div><br>\r\n                                          \r\n                    </div>\r\n                </div>               \r\n            </div>            \r\n            <div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;background-color: #fff;\">\r\n                <a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">\r\n                    <img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">\r\n                </a>\r\n                公司名稱：日日毛掌股份有限公司<br>\r\n                客服電話：02-6605-7606<br>\r\n                Email：pawsday888@gmail.com<br>\r\n                營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>\r\n                登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>\r\n                <div style=\"text-align: center; margin-top:20px\">\r\n                    © 2022 PawsDay All rights reserved.\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>";
            var response = await SendEmail(email);
        }

        public async Task UpdateStatus(EmailStatusDto input)
        {
            var email = GetEmailApi(input.EmailContentDTO);
            email.EmailTitle = "PawsDay 權限異動通知";
            email.EmailContent = $"<div style=\"background-color: #bbbbbb; width: 100%; box-sizing: border-box; padding: 20px 5px;line-height:1.5; font-family: 'Noto Sans TC', 'Microsoft YaHei', 'sans-serif';\">       \r\n\t<div style=\"background-color: #ffffff; max-width:480px; margin: auto;\">        \r\n\t\t<div style=\"margin: auto; width:150px; background-color: #fff;\">               \r\n\t\t\t<img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">          \r\n\t\t\t</div>          \r\n\t\t\t <div style=\"background-color: #f1c493; color: #fff; text-align: center; box-sizing: border-box; padding: 15px;\">官方通知 </div>            \r\n\t\t\t <div style=\"margin:20px 10px; background-color: #fff;\">              \r\n\t\t\t\t<div style=\"font-size: 14px; margin-bottom: 10px;\">親愛的 {input.EmailContentDTO.UserName} 您好：</div>      \r\n\t\t\t\t<div style=\"font-size: 14px; margin-bottom: 10px;\">您的權限異動如下 </div>               \r\n\t\t\t\t<div style=\" box-sizing: border-box; padding: 10px 10px;font-size: 14px; margin-bottom: 20px;\">                \r\n\t\t\t\t\t<div style=>                       \r\n\t\t\t\t\t\t<span style=\" font-weight: 700; margin-bottom: 15px; display: inline-block; \"> {input.StatusContent}                       \r\n\t\t\t\t\t\t</span>            \r\n\t\t\t\t\t</div>                \r\n\t\t\t\t</div>                       \r\n\t\t\t</div>                        \r\n\t\t<div style=\"border-top: 1px solid #a26d39; font-size: 12px; box-sizing: border-box;padding: 15px 10px;background-color: #fff;\">                \r\n\t\t\t<a href=\"{_httpContext.Request.Scheme}://{_httpContext.Request.Host.Value}\" style=\"margin: auto; width:100px; display: block;\">                   \r\n\t\t\t<img src=\"https://res.cloudinary.com/dnsu1sjml/image/upload/v1665165803/logo_eo0ibi.png\" width=\"100%\">                \r\n\t\t\t</a>公司名稱：日日毛掌股份有限公司<br>                \r\n\t\t\t客服電話：02-6605-7606<br>                \r\n\t\t\tEmail：pawsday888@gmail.com<br>                \r\n\t\t\t營業時間：週一至週五 9:00~18:00（例假日暫不提供電話服務）<br>                \r\n\t\t\t登記地址：106台北市大安區忠孝東路三段96號11號樓之1<br>                \r\n\t\t\t<div style=\"text-align: center; margin-top:20px\"> © 2022 PawsDay All rights reserved.</div>            \r\n\t\t</div>        \r\n\t</div>    \r\n</div>";
            var response = await SendEmail(email);
        }

    }
}

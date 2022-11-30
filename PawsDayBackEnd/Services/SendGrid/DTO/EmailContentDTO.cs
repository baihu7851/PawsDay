using SendGrid.Helpers.Mail;
using SendGrid;

namespace PawsDayBackEnd.Services.SendGrid.DTO
{
    public class EmailContentDTO
    {
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string RouteUrl { get; set; }
    }
    public class EmailALLDTO
    {
        public EmailContentDTO EmailContentDTO { get; set; }
        public EmailAddress PawsDay { get; set; }
        public SendGridClient Client { get; set; }
        public EmailAddress ToUser { get; set; }
        public string PlainTextContent { get; set; }
        public string EmailTitle { get; set; }
        public string EmailContent { get; set; }


    }
}

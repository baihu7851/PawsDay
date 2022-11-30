
namespace PawsDayBackEnd.Services.SendGrid.DTO
{
    public class EmailContactUsDTO
    {
        public EmailContentDTO EmailContentDTO { get; set; }
        public string ContactTitle { get; set; }
        public string ContactContent { get; set; }
        public string AnswerContent { get; set; }
    }
}

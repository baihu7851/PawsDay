namespace PawsDay.Services.SendGridServices.DTO
{
    public class EmailOrderDTO
    {
        public EmailContentDTO ContentDTO { get; set; }
        public string SitterName { get; set; }
        public string MemberName { get; set; }
        public string ServiceName { get; set; }
        public string OrderNum { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public EmailCancelOrderDTO EmailCancelOrderDTO { get; set; }

    }
    public class EmailCancelOrderDTO
    { 
        public decimal CancelBackAmount { get; set; }
        public string CancelReason { get; set; }
        public string CreatrTime { get; set; }

    }
}

using ApplicationCore.Common;

namespace PawsDayBackEnd.DTO.SitterChart
{
    public class StatusQuantityDto
    {
        public int AwaitToCheck { get; set; }
        public int Approved { get; set; }
        public int Reject { get; set; }
        public int Suspend { get; set; }
    }
}
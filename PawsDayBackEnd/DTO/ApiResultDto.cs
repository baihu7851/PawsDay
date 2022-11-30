using System.Net.NetworkInformation;

namespace PawsDayBackEnd.DTO
{
    public class ApiResultDto
    {
        //建構式多載(如帶data則預設為Success)
        public ApiResultDto()
        {
        }

        public ApiResultDto(object data)
        {
            Status = StatusCode.Success;
            Data = data;
        }

        public StatusCode Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public enum StatusCode
    {
        Success = 20000,
        Failed = 40000
    }
}

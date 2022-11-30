namespace PawsDay.Models.SitterCenter.WebApi
{
    public class ResultDto
    {
        public ResultDto()
        {

        }

        public ResultDto(object data)
        {
            IsSuccess = true;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

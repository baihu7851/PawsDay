namespace PawsDay.Models.SitterCenter
{
    public class TransResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

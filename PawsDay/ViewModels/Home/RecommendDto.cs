namespace PawsDay.ViewModels.Home
{
    public class RecommendDto
    {
        public int ProductId { get; set; }
        public int ProductStatus { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public int SitterID { get; set; }
        public string Image { get; set; }
        public int EvaluationCount { get; set; }
        public decimal EvaluationAvg { get; set; }
        public int OrderCount { get; set; }
        public string County { get; set; }
        public decimal Price { get; set; }
    }
}

namespace PawsDay.ViewModels.Home
{
    public class RecommendViewModel
    {
        public int ProductId { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string Image { get; set; }
        public double EvaluationAverage { get; set; }
        public int EvaluationQuantity { get; set; }
        public int OrderQuantity { get; set; }
        public bool Collect { get; set; }
        public string Price { get; set; }
        public string County { get; set; }
        public int Rank { get; set; }
    }
}

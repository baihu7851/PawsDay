namespace PawsDay.Models.SitterCenter.WebApi
{
    public class ProductDetailWithPriceDto
    {
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public decimal Price { get; set; }
        public decimal NightPrice { get; set; }
    }
}

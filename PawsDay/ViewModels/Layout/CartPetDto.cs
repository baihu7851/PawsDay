namespace PawsDay.ViewModels.Layout
{
    public class CartPetDto
    {
        public int PetTypeId { get; set; }
        public string PetType { get; set; }
        public int ShapeTypeId { get; set; }
        public string ShapeType { get; set; }
        public decimal Price { get; set; }
        public decimal OvernightPrice { get; set; }
    }
}

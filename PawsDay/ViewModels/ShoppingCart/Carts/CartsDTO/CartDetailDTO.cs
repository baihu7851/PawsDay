namespace PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO
{
    public class CartDetailDTO
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}

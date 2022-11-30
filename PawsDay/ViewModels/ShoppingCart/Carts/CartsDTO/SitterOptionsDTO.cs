using PawsDay.ViewModels.Product;
using System.Collections.Generic;

namespace PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO
{
    public class SitterOptionsDTO
    {
        public List<string> PetTypes { get; set; }
        public List<ChoosePetTypeDto> TypeOptions { get; set; }
    }
}

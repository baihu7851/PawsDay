using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.ViewModels.Product;
using System.Collections.Generic;

namespace PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO
{
    public class CreateSelectedDTO
    {
        public List<string> ServicePetTypes { get; set; }
        public List<ChoosePetTypeDto> ServiceShapeTypes { get; set; }
    }
}

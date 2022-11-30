using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using System.Collections.Generic;

namespace PawsDay.ViewModels.ShoppingCart.Carts
{
    public class ListCartItemViewModel
    {
        public int UserId { get; set; }

        public List<CartItemViewModel> validCartItemList { get; set; }
        public List<CartItemViewModel> expiredCartItemList { get; set; }
        

        public string IndexOfSelectedItem { get; set; }
        public string FinalTotalPrice { get; set; }
        
    }
}

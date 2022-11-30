using PawsDay.ViewModels.ShoppingCart.Carts;
using System.Collections.Generic;

namespace PawsDay.ViewModels.ShoppingCart.OrderPlaced
{
    public class OrderPlacedViewModel
    {
        public string BookerName { get; set; }
        public string BookerTel { get; set; }
        public string BookerEmail { get; set; }
        //public CartItemViewModel PayItem { get; set; }
        public List<CartItemViewModel> ListOfPayItems { get; set; }

        public string CompanyTaxID { get; set; }
        public string CompanyTitleName { get; set; }
    }
}

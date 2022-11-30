using System.Collections.Generic;

namespace PawsDay.ViewModels.ShoppingCart.OrderPlaced.DTO
{
    public class ReturnBlocToDTO
    {
        public string OrderIds { get; set; }
        public string CartIds { get; set; }
        public string TxId { get; set; }
    }
}

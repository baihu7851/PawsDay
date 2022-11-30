using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using System.Collections.Generic;

namespace PawsDay.ViewModels.ShoppingCart.OrderPlaced.DTO
{
    public class OrderDetailDTO
    {
        public string OrderNumber { get; set; }
        public string TxId { get; set; } //for blocTo only
        public string PhotoUrl { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public decimal CartPrice { get; set; }
        public List<PetTitleDTO> ListOfPets { get; set; }
    }
}

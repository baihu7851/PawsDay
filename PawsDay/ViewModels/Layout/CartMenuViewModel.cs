using ApplicationCore.Common;
using System.Collections.Generic;

namespace PawsDay.ViewModels.Layout
{
    public class CartMenuViewModel
    {
        public int ProductId { get; set; }
        public string SitterName { get; set; }
        public string ServiceType { get; set; }
        public string Image { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public string TotalPrice { get; set; }
        public List<CartPetDto> CartPets { get; set; }
    }
}

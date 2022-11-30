using ApplicationCore.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.Product
{
    public class ProductToCartDto
    {
        public int MemberId { get; set; }
        [Required]
        public int SelectedId { get; set; }
        [Required]
        public string SelectedDay { get; set; }
        [Required]
        public string SelectedTime { get; set; }
        [Required]
        public string SelectedCounty { get; set; }
        [Required]
        public string SelectedDistrict { get; set; }
        [Required]
        public string SelectedShapeTypes { get; set; }
        public string SelectedPrice { get; set; }
    }
}
  
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;
using PawsDay.ViewModels.ShoppingCart.Carts;

namespace PawsDay.ViewModels.ShoppingCart
{
    public class BookingInformationViewModel
    {
        //訂購人
        //[Required(ErrorMessage = "請填寫訂購人全名")]
        public string BookingPersonName { get; set; }
        //[Required(ErrorMessage = "請填寫訂購人電話")]
        public string BookingPersonTel { get; set; }
        //[Required(ErrorMessage = "請填寫訂購人Email")]
        public string BookingPersonEmail { get; set; }


        //購物項目
        public List<CartItemViewModel> CartItemList { get; set; }


        //預設項目

        public string MemberName { get; set; }
        public string MemberTel { get; set; }
        public string MemberEmail { get; set; } 
        public string MemberAddress { get; set; }

        public List<PetInfomation> MemberPetsList { get; set; }


    }
}

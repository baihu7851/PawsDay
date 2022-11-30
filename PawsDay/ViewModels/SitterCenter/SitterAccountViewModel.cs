using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Services.ShoppingCart;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterAccountViewModel
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public int MemberID { get; set; }
        [Required(ErrorMessage ="請選擇銀行")]
        public string Bank { get; set; }

        [Required(ErrorMessage ="請輸入正確銀行帳戶")]
        [MaxLength(16)]
        public string Account { get; set; }

        public IEnumerable<SelectListItem> BankList { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.ViewModels.Account
{
    public class LoginViewModel
    {
        // by https://blog.poychang.net/note-regular-expression/
        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "信箱")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email")]
        [MaxLength(100, ErrorMessage = "長度不可超過 100 個字元")]   
        public string Email { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "密碼")]
        [MaxLength(12,ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [MinLength(6,ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",  ErrorMessage = "密碼最少要有一個英文與數字")]
        public string Password { get; set; }

        public bool IsRemember { get; set; } =true;

    }
}

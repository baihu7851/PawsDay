using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.ViewModels.Account
{
    public class ForgotViewModel
    {
        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "信箱")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email")]
        [MaxLength(100, ErrorMessage = "長度不可超過 100 個字元")]
        public string Email { get; set; }

    }
}

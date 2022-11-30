using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PawsDay.ViewModels.MemberCenter
{
    public class AccountSafetyViewModel
    {        
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "舊密碼")]
        [MaxLength(12, ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [MinLength(6, ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,12}$", ErrorMessage = "密碼最少要有一個英文與數字")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "新密碼")]
        [MaxLength(12, ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [MinLength(6, ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,12}$", ErrorMessage = "密碼最少要有一個英文與數字")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "確認密碼")]
        [MaxLength(12, ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [MinLength(6, ErrorMessage = "輸入英文及數字6~12字元內組合")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,12}$", ErrorMessage = "密碼最少要有一個英文與數字")]
        [DataType(DataType.Password)]
        public string NewCheckPassword { get; set; }
        public UpdateAccount updateAccount { get; set; }
    }
    public class UpdateAccount
    {        
        public bool IsUpdate { get; set; }
        public string Message { get; set; }
    }
}

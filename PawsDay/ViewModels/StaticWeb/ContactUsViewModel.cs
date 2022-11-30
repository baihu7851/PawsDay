using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.StaticWeb
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "必填欄位")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "必填欄位")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email")]
        public string Mail{get;set;}

        [Required(ErrorMessage = "必填欄位")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "請輸入09開頭的10碼手機號碼")]
        public string Phone { get;set;}

        [Required(ErrorMessage = "必填欄位")]
        public string Title { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public string ContactContent { get; set; }
    }
}

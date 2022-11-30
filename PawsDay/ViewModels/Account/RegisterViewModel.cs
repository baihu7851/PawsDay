using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.ViewModels.Account
{

    public class RegisterViewModel
    {
        //public string JsonText { get; set; }

        public int AccountInfoId { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "姓名")]
        [MaxLength(20, ErrorMessage = "長度不可超過 20 個字元")]
        public string Name { get; set; }

        [Display(Name = "暱稱")]
        [MaxLength(20, ErrorMessage = "長度不可超過 20 個字元")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "性別")]
        public string Gender { get; set; }
        public List<SelectListItem> GenderList { get; set; } =
            new List<SelectListItem>() 
            {
                new SelectListItem { Text = "男", Value = "true" },
                new SelectListItem { Text = "女", Value = "false" } 
            };


        [Display(Name = "出生日期")]
        public DateTime Birth { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "縣市")]
        public string County { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public string District { get; set; }

        //public List<CountyandDistrictDto> DistrictList { get; set; }

        public List<CountyDto> CountyList { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        [Display(Name = "地址")]
        [MaxLength(40, ErrorMessage = "長度不可超過 40 個字元")]
        public string Address { get; set; }

        [Required(ErrorMessage = "必填欄位")] 
        [RegularExpression(@"^09[0-9]{8}$", ErrorMessage = "需為09xxxxxxxx格式")]
        [MaxLength(10, ErrorMessage = "長度不可超過 10 個字元")]
        [Display(Name = "電話號碼")]
        public string Phone { get; set; }


    }
}

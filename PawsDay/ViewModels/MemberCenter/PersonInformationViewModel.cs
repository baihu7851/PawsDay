using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Models.Account;
using PawsDay.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.ViewModels.MemberCenter
{
    public class PersonInformationViewModel
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        //[Display(Name = "暱稱")]
        public string NickName { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "性別")]
        public bool? Gender { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        [Display(Name = "出生日期")]
        [DataType(DataType.Date)]
        
        public DateTime? Birth { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "縣市")]        
        public int? MemberCounty { get; set; }        
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "行政區")]
        public int? MemberDistrict { get; set; }
        public List<CountyDto> CountyList { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "電話號碼")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "聯絡Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime? EditTime { get; set; }
        public int RegisterType { get; set; }
    }
}

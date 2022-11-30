using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.Models.MemberCenter;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.ViewModels.MemberCenter
{
    public class MemberPetInfoViewModel
    {
        public int PetID { get; set; }

        [Display(Name = "毛孩名稱")]
        [Required(ErrorMessage = "必填欄位")]
        public string PetName { get; set; } //名稱

        [Display(Name = "毛孩性別")]
        [Required(ErrorMessage = "必填欄位")]
        public string PetGender { get; set; } //性別
        public IEnumerable<SelectListItem> PetGenderList { get; set; }

        [Display(Name = "毛孩類別")]
        [Required(ErrorMessage = "必填欄位")]
        public string PetType { get; set; } //類別
        public IEnumerable<SelectListItem> PetTypeList { get; set; }

        [Display(Name = "毛孩體型")]
        [Required(ErrorMessage = "必填欄位")]
        public string ShapeType { get; set; } //體型
        public IEnumerable<SelectListItem> ShapeTypeList { get; set; }

        [Display(Name = "毛孩出生年")]
        [Required(ErrorMessage = "必填欄位")]
        public int BirthYear { get; set; } //出生年

        [Display(Name = "毛孩出生月")]
        public int? BirthMonth { get; set; } //出生月
        public IEnumerable<SelectListItem> BirthMonthList { get; set; }

        [Display(Name = "毛孩個性表(可複選)")]
        [Required(ErrorMessage = "必填欄位")]
        public List<string> PetDispositionType { get; set; } //個性名稱
        public List<SelectListItem> PetDispositionList { get; set; } //個性選單表

        [Display(Name = "是否已結紮")]
        [Required(ErrorMessage = "必填欄位")]
        public string Ligation { get; set; } //結紮
        public List<SelectListItem> LigationList { get; set; }

        [Display(Name = "是否有定期打疫苗")]
        [Required(ErrorMessage = "必填欄位")]
        public string Vaccine { get; set; } //疫苗
        public List<SelectListItem> VaccineList { get; set; }

        [Display(Name = "補充描述")]
        public string Description { get; set; } //描述

    }

    //public class PetDispositionType
    //{
    //    public int DispositionId { get; set; }
    //    public string DispositionName { get; set; }
    //    public bool DispositionChecked { get; set; }
    //}
}

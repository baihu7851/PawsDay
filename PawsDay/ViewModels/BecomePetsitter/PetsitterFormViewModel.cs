using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.BecomePetsitter
{
    public class PetsitterFormViewModel
    {
        public int MemberId { get; set; }

        [Required(ErrorMessage = "請輸入正確的身份證字號")]
        [RegularExpression("^[A-Za-z]{1}[1-2]{1}[0-9]{8}$")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public string PetExperience { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public string IdCardFront { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public string IdCardBack { get; set; }

        public List<string> License { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public List<int> Quiz { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public List<AptitudeType> Aptitude { get; set; }

        public List<int> QuestionId { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public List<string> Answer { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    }
}
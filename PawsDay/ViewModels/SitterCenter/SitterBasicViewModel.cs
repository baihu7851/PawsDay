using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterBasicViewModel
    {
        public bool IsValid { get; set; }

        public string Message { get; set; }
        public int MemberID { get; set; }
        [Required(ErrorMessage = "請輸入20字以內保姆暱稱")]
        [MaxLength(20)]
        public string SitterName { get; set; }
        [Required(ErrorMessage = "請輸入30~500字保姆介紹")]
        [MinLength(30)]
        [MaxLength(500)]
        public string SitterDescription { get; set; }

    }
}

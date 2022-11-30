using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.ViewModels.MemberCenter
{
    public class OrderEvaluationViewModel
    {
        [Required(ErrorMessage = "此欄位必填")]
        public int Evaluation { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        [Display(Name = "評論內容")]
        public string Message { get; set; }
        public MemberCenterOrderSidebarViewModel memberCenterOrderSidebarViewModel { get; set; }
        public string OrderNum { get; set; }
        public int OrderId { get; set; }
    }
}

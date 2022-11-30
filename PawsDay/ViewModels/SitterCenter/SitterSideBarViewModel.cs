using PawsDay.ViewModels.MemberCenter;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterSideBarViewModel
    {
        public int MemberID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public bool IsEmail { get; set; }

        public RegisterTypeDTO RegisterTypeDTO { get; set; }
    }
}

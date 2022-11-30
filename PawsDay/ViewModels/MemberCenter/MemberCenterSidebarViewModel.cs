using System;
using System.Collections.Generic;

namespace PawsDay.ViewModels.MemberCenter
{
    public class MemberCenterSidebarViewModel
    {
        public int MemberId { get; set; }   
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberImage { get; set; }
        public string MemberInputImage { get; set; }
        public int SitterStatu { get; set; }
        public bool IsSitter { get; set; }
        public bool IsEmail { get; set; }
        public RegisterTypeDTO RegisterTypeDTO { get; set; }

    }
    public class RegisterTypeDTO
    { 
        public string Name { get; set; }
        public string Image { get; set; }
    }
}

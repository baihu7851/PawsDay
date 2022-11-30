using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDay.Models.SitterCenter.WebApi;
using PawsDay.Models;
using PawsDay.ViewModels.MemberCenter;
using PawsDay.ViewModels.SitterCenter;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PawsDay.Services.MemberCenter
{
    public class MemberCenterSidebarServices
    {
        private readonly IRepository<Member> _member;
        private readonly IRepository<RegisterSitter> _sitter;
        private readonly IRepository<AccountInfo> _accountInfo;

        public MemberCenterSidebarServices(IRepository<Member> member, IRepository<RegisterSitter> sitter, IRepository<AccountInfo> accountInfo)
        {
            _member = member;
            _sitter = sitter;
            _accountInfo = accountInfo;
        }


        public MemberCenterSidebarViewModel Partial(int userId)
        {            
            var member = _member.GetAllReadOnly().First(x => x.MemberId == userId);
            var sitter= _sitter.GetAllReadOnly().FirstOrDefault(x => x.MemberId == userId);
            
            var user= new MemberCenterSidebarViewModel
            {
                MemberId = member.MemberId,
                MemberName = member.Name,
                MemberImage = member.ProfileImage,
                IsEmail = member.RegisterType==(int)RegisterTypes.Email,
                RegisterTypeDTO = GetRegisterType(member.RegisterType)
            };
            if (member.RegisterType == (int)RegisterTypes.Email)
            {
                user.MemberEmail = _accountInfo.GetAllReadOnly().First(x => x.AccountInfoId == member.AccountInfoId).Account;
            }
            if (sitter != null)
            {
                user.IsSitter = true;
                user.SitterStatu = sitter.RegisterStatus;
            }
            else { user.IsSitter = false; }

            return user;
        }
        private RegisterTypeDTO GetRegisterType(int type)
        {
            switch (type)
            {
                case (int)RegisterTypes.Facebook:
                    return new RegisterTypeDTO { Name="FaceBook",Image= "~/images/FaceBook.jpg" };
                case (int)RegisterTypes.Line:
                    return new RegisterTypeDTO { Name = "Line", Image = "~/images/line.jpg" };
                case (int)RegisterTypes.Google:
                    return new RegisterTypeDTO { Name = "Google", Image = "~/images/google.png" };
                default:
                    return null;
            }
        }

        public async Task<ResultDto> UpdateUserImage(UserImageDto request)
        {
            var response = new ResultDto();
            var info = _member.GetAllReadOnly().First(s => s.MemberId == request.Userid);

            info.ProfileImage = request.Image;
            try
            {
                var res = await _member.UpdateAsync(info);
                return new ResultDto(res);
            }
            catch (Exception ex)
            {
                response.Message = $"更新失敗:{ex.Message}";
                return response;
            }
        }
    }
}

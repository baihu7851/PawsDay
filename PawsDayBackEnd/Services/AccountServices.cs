using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PawsDayBackEnd.DTO.Account;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.Helpers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using ApplicationCore.Common;
using System.Xml.Linq;

namespace PawsDayBackEnd.Services
{
    public class AccountServices
    {

        private readonly IRepository<UserRole> _UserRole;
        private readonly IRepository<Member> _Member;
        private readonly IRepository<AccountInfo> _AccountInfo;
        private readonly IAppPasswordHasher _appPasswordHasher;

        public AccountServices(IRepository<UserRole> userrole, IRepository<Member> member, IRepository<AccountInfo> accountinfo, IAppPasswordHasher appPasswordHasher)
        {
            _UserRole = userrole;
            _Member = member;
            _AccountInfo = accountinfo;
            _appPasswordHasher = appPasswordHasher;
        }


        public VerifyResponse VerifyAccount(LoginDTO request)
        {
            //檢核是否為會員
            var accountinfo = _AccountInfo.GetAllReadOnly().FirstOrDefault(a => a.Account == request.UserName);
            if (accountinfo == null) { return VerifyResponse.IsNotUser; }
            //檢核是否為管理者
            var userid = _Member.GetAllReadOnly().First(m => m.AccountInfoId == accountinfo.AccountInfoId).MemberId;
            var role = _UserRole.GetAllReadOnly().Any(r => r.UserId == userid && r.RoleType == (int)UserType.Admin);
            if (!role) { return VerifyResponse.IsNotAdmin; }
            //檢核密碼
            if (_appPasswordHasher.HashPasseword(request.Password) != accountinfo.Password) { return VerifyResponse.PassWordError; }
            //都通過=驗證成功
            return VerifyResponse.VerifySuccess;
        }

        public AccountInfoDto GetUserInfo(string email)
        {
            var response = new AccountInfoDto();
            var member = _Member.GetAllReadOnly().FirstOrDefault(m => m.Email == email);
            if (member != null) 
            {
                response.Name = member.Name;
                response.ImgUrl = member.ProfileImage;
            }
            return response;
        }
    }

    public enum VerifyResponse
    {
        IsNotUser = 0,
        IsNotAdmin = 1,
        PassWordError = 2,
        VerifySuccess = 3
    }
}

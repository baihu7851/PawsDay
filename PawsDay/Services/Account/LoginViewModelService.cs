using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;

using PawsDay.Interfaces.Account;
using PawsDay.Services.SendGridServices;
using PawsDay.Services.SendGridServices.DTO;
using PawsDay.ViewModels.Account;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PawsDay.Services.Account
{
    public class LoginViewModelService : IAccountManager
    {


        private readonly IRepository<AccountInfo> _accountInfoRepository;
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<LineRegister> _lineRegisterRepository;
        private readonly IRepository<GoogleRegister> _googleRegisterRepository;

        private readonly IAppPasswordHasher _appPasswordHasher;
        private readonly HttpContext _httpContext;
        private readonly SendGridService _sendGridService;

        public LoginViewModelService(IRepository<AccountInfo> accountInfoRepository, IRepository<Member> memberRepository, IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository, IRepository<LineRegister> lineRegisterRepository, IRepository<GoogleRegister> googleRegisterRepository, IAppPasswordHasher appPasswordHasher, IHttpContextAccessor httpContextAccessor, SendGridService sendGridService)
        {
            _accountInfoRepository = accountInfoRepository;
            _memberRepository = memberRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _lineRegisterRepository = lineRegisterRepository;
            _appPasswordHasher = appPasswordHasher;
            _httpContext = httpContextAccessor.HttpContext;
            _sendGridService = sendGridService;
            _googleRegisterRepository = googleRegisterRepository;

        }


        public bool IsExistUser(AccountInfo accountInfo)
        {
            return _accountInfoRepository.Any(a => a.Account == accountInfo.Account);
        }

        public bool CanUserLogin(AccountInfo accountInfo)
        {
            return _accountInfoRepository.Any(a => a.Account == accountInfo.Account && a.Password == _appPasswordHasher.HashPasseword(accountInfo.Password)); ;
        }

        public async Task<AccountInfo> GetAccountByAccuntInfoAsync(AccountInfo accountInfo)
        {
            return await _accountInfoRepository.SingleOrDefaultAsync(a => a.Account == accountInfo.Account && a.Password == _appPasswordHasher.HashPasseword(accountInfo.Password));
        }

        public async Task<AccountInfo> GetAccountInfoByIdAsync(int accountInfoId)
        {
            var accountInfo = await _accountInfoRepository.SingleOrDefaultAsync(a => a.AccountInfoId == accountInfoId);
            return accountInfo;
        }

        public async Task<AccountInfo> GetAccountInfoByEmailAsync(string email)
        {
            var accountInfo=await _accountInfoRepository.SingleOrDefaultAsync(a=>a.Account==email);
            return accountInfo;
        }

        public async Task<LineRegister> GetLineInfoByLineUserIdFromDb(string lineUserId)
        {
            var lineUserInfo = await _lineRegisterRepository.GetAllReadOnly().SingleOrDefaultAsync(a => a.Account == lineUserId);
            return lineUserInfo;
        }
        public async Task<GoogleRegister> GetGoogleInfoByGoogleUserIdFromDb(string googleUserId)
        {
            var googleUserInfo = await _googleRegisterRepository.GetAllReadOnly().SingleOrDefaultAsync(a => a.Account == googleUserId);
            return googleUserInfo;
        }

        public async Task<LineRegister> CreateLineUserInfo(string lineUserId, int memberId)
        {
            var lineUserInfo = new LineRegister { Account = lineUserId, MemberId = memberId };
            var lineUserInfoFromDb = await _lineRegisterRepository.AddAsync(lineUserInfo);


            return lineUserInfoFromDb;
        }

       public async Task<GoogleRegister> CreateGoogleUserInfo(string id, int memberId)
        {
            var googleUserInfo = new GoogleRegister { Account = id, MemberId = memberId };
            var googleUserInfoFromDb = await _googleRegisterRepository.AddAsync(googleUserInfo);


            return googleUserInfoFromDb;
        }


        public async Task<Member> CreateMemberInfoByLine(string email, string name, string picUrl)
        {
            var member = new Member
            {
                RegisterType = 2,
                Name = name,
                Email = email,
                ProfileImage = picUrl,
                Status = 1,
                IsDelete = false,
                CreateTime = DateTime.UtcNow,

            };

            var memberFromDb = await _memberRepository.AddAsync(member);

            await CreateUserRoleAsync(memberFromDb.MemberId);

            return memberFromDb;

        }

        public async Task<Member> CreateMemberInfoByGoogle(string email, string name, string picUrl)
        {
            var member = new Member
            {
                RegisterType = 3,
                Name = name,
                Email = email,
                ProfileImage = picUrl,
                Status = 1,
                IsDelete = false,
                CreateTime = DateTime.UtcNow,

            };

            var memberFromDb = await _memberRepository.AddAsync(member);

            await CreateUserRoleAsync(memberFromDb.MemberId);

            return memberFromDb;

        }

        



        public async Task<Member> GetMemberByMemberId(int memberId)
        {
            return await _memberRepository.GetByIdAsnyc(memberId);
        }



        public AccountInfo UpdateExpirationTime(int accountInfoId)
        {
            var userAccountInfo = _accountInfoRepository.GetById(accountInfoId);
            userAccountInfo.ExpirationTime = DateTime.UtcNow.AddMinutes(30);
            var updateUserAccountInfo = _accountInfoRepository.Update(userAccountInfo);
            return updateUserAccountInfo;
        }

        public async Task<Member> GetMemberInfoByAccountInfoAsync(AccountInfo accountInfo)
        {
            return await _memberRepository.SingleOrDefaultAsync(m => m.AccountInfoId == accountInfo.AccountInfoId);
        }


        public async Task LoginAsync(Member member, bool isPersistent = true)
        {
            var claimsIdentity = BulidClaimsIdentity(member);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = isPersistent
            });
        }

        private static ClaimsIdentity SetClaimsIdentiy(string memberId, IEnumerable<Role> roles)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, memberId));

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            }

            return identity;
        }


        private ClaimsIdentity BulidClaimsIdentity(Member member)
        {

            var userRole = _userRoleRepository.Where(x => x.UserId == member.MemberId).Select(x => x.RoleType).ToList();

            var roles = _roleRepository.Where(r => userRole.Contains(r.RoleId)).ToList();

            var memberId = member.MemberId.ToString();

            var userIdentity = SetClaimsIdentiy(memberId, roles);

            return userIdentity;

        }

        public async Task SignOutAsync()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _httpContext.User.Identities != null && _httpContext.User.Identity.IsAuthenticated;
        }

        public async Task<AccountInfo> SignUpAsync(SignUpViewModel input)
        {
            var accountInfo = new AccountInfo()
            {
                Account = input.Email,
                Password = _appPasswordHasher.HashPasseword(input.Password),
                CreateTime = DateTime.UtcNow,
                ExpirationTime = DateTime.UtcNow.AddMinutes(30)

            };

         return    await _accountInfoRepository.AddAsync(accountInfo);


        }



        public async Task<AccountInfo> GetUser(AccountInfo accountInfo)
        {
            return await _accountInfoRepository.SingleOrDefaultAsync(u => u.Account == accountInfo.Account && u.Password == _appPasswordHasher.HashPasseword(accountInfo.Password));
        }

        public async Task<Member> RegisterMember(RegisterViewModel registerViewModel)
        {
            var userEmail = await GetAccountInfoByIdAsync(registerViewModel.AccountInfoId);

            // 建立會員資料
            var member = new Member()
            {
                RegisterType = 1,
                Name = registerViewModel.Name,
                NickName = registerViewModel.NickName,
                Sex = registerViewModel.Gender == "true" ? true : false,
                Birth = registerViewModel.Birth,
                County = int.Parse(registerViewModel.County),
                District = int.Parse(registerViewModel.District),
                Address = registerViewModel.Address,
                Phone = registerViewModel.Phone,
                Email = userEmail.Account,
                Status = 1,
                IsDelete = false,
                CreateTime = DateTime.UtcNow,
                AccountInfoId = registerViewModel.AccountInfoId,

            };

            // 將資料儲存到 Member Table
            var memberFromDB = await _memberRepository.AddAsync(member);

            var memberUserRole = await CreateUserRoleAsync(memberFromDB.MemberId);


            // 修改 userAccountInfo 的 Verify 欄位，改成已驗證
            var userAccountInfo = _accountInfoRepository.GetAll().SingleOrDefault(x => x.AccountInfoId == registerViewModel.AccountInfoId);

            userAccountInfo.Verify = true;

            await _accountInfoRepository.UpdateAsync(userAccountInfo);

            return memberFromDB;
        }


        private async Task<UserRole> CreateUserRoleAsync(int memberid)
        {
            // 建立 UserRole
            var userRole = new UserRole()
            {
                UserId = memberid,
                RoleType = 1,
            };


            //  UserRole 新增到資料庫
            return await _userRoleRepository.AddAsync(userRole);
        }

        public int GetLoginMemberId()
        {
            var isMember = int.TryParse(_httpContext.User.Identity.Name, out int memberId);

            if (!isMember)
            {
                memberId = 0;
            }

            return memberId;
        }

        public async Task<bool> SendRegisterEmailAsync(AccountInfo accountInfoFromDb)
        {
            var emailInfo= sendEmail(accountInfoFromDb);

            return await _sendGridService.CheckRegisterEmailAsync(emailInfo);
        }

        public async Task<bool> SendForgotPasswordEmailAsync(AccountInfo userAccountInfo)
        {
            var emailInfo=sendEmail(userAccountInfo);            

            return await _sendGridService.ForgotPasswordEmailAsync(emailInfo);


        }

        public bool ConfirmData(int accountInfoId, string intputExpirationTime)
        {
            return _accountInfoRepository.GetById(accountInfoId).ExpirationTime.ToString("yyyy-MM-dd-HH-mm-ss") == intputExpirationTime;
        }


        public ResetPasswordViewModel UpdatePasswordByForgotPassword(ResetPasswordViewModel input)
        {
            var accountInfo = _accountInfoRepository.GetById(input.AccountInfoId);

            input.UpdateAccount=new UpdateAccountByResetPassword();     

            try
            {
                accountInfo.Password = _appPasswordHasher.HashPasseword(input.NewPassword);
                input.UpdateAccount.Message = "密碼更新成功";
                input.UpdateAccount.IsUpdate = true;
                var updateAccountInfo=_accountInfoRepository.Update(accountInfo);
            }
            catch
            {
                input.UpdateAccount.IsUpdate = false;
                input.UpdateAccount.Message = "密碼更新失敗，請在試一次";
            }
            return input;
        }



        private EmailContentDTO sendEmail(AccountInfo accountInfoFromDb)
        {
            var sendEmailInfo = new EmailContentDTO
            {
                UserEmail = accountInfoFromDb.Account,
                UserName = string.Empty,
                RouteUrl = $"/{accountInfoFromDb.AccountInfoId}/{accountInfoFromDb.ExpirationTime.ToString("yyyy-MM-dd-HH-mm-ss")}"
            };

            return sendEmailInfo;


        }


    }
}

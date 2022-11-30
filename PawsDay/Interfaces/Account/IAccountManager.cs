using ApplicationCore.Entities;
using PawsDay.ViewModels.Account;
using System.Threading.Tasks;

namespace PawsDay.Interfaces.Account
{
    public interface IAccountManager
    {
        bool IsExistUser(AccountInfo accountInfo);
        bool CanUserLogin(AccountInfo accountInfo);
        Task<AccountInfo> GetAccountByAccuntInfoAsync(AccountInfo accountInfo);
        Task<AccountInfo> GetAccountInfoByEmailAsync(string email);
        Task<bool> SendForgotPasswordEmailAsync(AccountInfo userAccountInfo);
        Task LoginAsync(Member member, bool isPersistent = true);
        Task SignOutAsync();
        bool IsAuthenticated();
        Task<AccountInfo> SignUpAsync(SignUpViewModel input);
        Task<AccountInfo> GetUser(AccountInfo accountInfo);
        Task<LineRegister> GetLineInfoByLineUserIdFromDb(string lineUserId);
        Task<LineRegister> CreateLineUserInfo(string lineUserId, int memberId);
        Task<GoogleRegister> GetGoogleInfoByGoogleUserIdFromDb(string googleUserId);
        Task<GoogleRegister> CreateGoogleUserInfo(string id, int memberId);
        Task<Member> CreateMemberInfoByGoogle(string email, string name, string picUrl);
        Task<Member> GetMemberByMemberId(int memberId);
        Task<Member> CreateMemberInfoByLine(string email, string name, string picUrl);
        Task<AccountInfo> GetAccountInfoByIdAsync(int accountInfoId);
        Task<Member> RegisterMember(RegisterViewModel registerViewModel);
        Task<Member> GetMemberInfoByAccountInfoAsync(AccountInfo accountInfo);
        AccountInfo UpdateExpirationTime(int accountInfoId);
        int GetLoginMemberId();
        Task<bool> SendRegisterEmailAsync(AccountInfo accountInfoFromDb);
        //UpdateAccountByResetPassword UpdatePasswordByForgotPassword(ResetPasswordViewModel input);
        ResetPasswordViewModel UpdatePasswordByForgotPassword(ResetPasswordViewModel input);
        bool ConfirmData(int accountInfoId, string intputExpirationTime);

    }
}

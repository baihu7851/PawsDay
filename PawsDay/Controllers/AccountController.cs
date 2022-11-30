
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PawsDay.Interfaces.Account;
using PawsDay.Services.SendGridServices;
using PawsDay.Services.SendGridServices.DTO;
using PawsDay.ViewModels.Account;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Http;

namespace PawsDay.Controllers
{

    public class AccountController : Controller
    {
        private IConfigurationSection _lineChannel_ID, _lineChannel_Secret, _lineCallBackURL;
        private IConfigurationSection _googleClient_ID, _googleClient_Secret, _googleCallBackURL;

        private readonly IAccountManager _accountManager;
        private readonly IRegisterViewModelService _registerViewModelService;
        private readonly SendGridService _sendGridService;
        private readonly string _homePageUrl = "/";


        public AccountController(IAccountManager accountManager, IRegisterViewModelService registerViewModelService, SendGridService sendGridService, IConfiguration config)
        {

            _accountManager = accountManager;
            _registerViewModelService = registerViewModelService;
            _sendGridService = sendGridService;
            _lineChannel_ID = config.GetSection("LINE-Login-Setting:channel_ID");
            _lineChannel_Secret = config.GetSection("LINE-Login-Setting:channel_Secret");
            _lineCallBackURL = config.GetSection("LINE-Login-Setting:callBackURL");
            _googleClient_ID = config.GetSection("Google-Login-Setting:client_id");
            _googleClient_Secret = config.GetSection("Google-Login-Setting:client_secret");
            _googleCallBackURL = config.GetSection("Google-Login-Setting:call_back_url");

        }

        //let isLoginPop = '@ViewBag.isLoginPop';
        //let isLoginEmailPop = '@ViewBag.isLoginEmailPop'
        //let isForgotPop = '@ViewBag.isForgotPop'
        //let isSignUpPop = '@ViewBag.isSignUp'

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            string nowUrl = getNowUrl() ?? "/";
            Response.Cookies.Append("returnUrl", returnUrl ?? $"{nowUrl}", new CookieOptions() { Expires = DateTime.UtcNow.AddMinutes(5) });
            TempData["isLoginPop"] = true;

            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginViewModel inputLoginVM)
        {
            var nowUrl = getNowUrl() ?? "/";
            var returnUrl = getReturnUrl() ?? nowUrl;

            if (!ModelState.IsValid)
            {
                TempData["isLoginError"] = true;
                TempData["LoginErrorMsg"] = "帳號或密碼錯誤";
                TempData["isLoginEmailPop"] = true;
                return Redirect($"{nowUrl}");
            }



            var accountInfo = new AccountInfo
            {
                Account = inputLoginVM.Email,
                Password = inputLoginVM.Password,
            };




            if (!_accountManager.CanUserLogin(accountInfo))
            {
                TempData["isLoginError"] = true;
                TempData["LoginErrorMsg"] = "帳號或密碼錯誤";
                TempData["isLoginEmailPop"] = true;
                return Redirect($"{nowUrl}");
            }

            var personalAccountInfo = await _accountManager.GetAccountByAccuntInfoAsync(accountInfo);

            if (personalAccountInfo is null)
            {
                TempData["isLoginError"] = true;
                TempData["LoginErrorMsg"] = "系統錯誤，請稍後再試！";
                TempData["isLoginEmailPop"] = true;
                return Redirect($"{nowUrl}");
            }

            if (personalAccountInfo.Verify == false)
            {
                TempData["isLoginError"] = true;
                TempData["LoginErrorMsg"] = "帳號還未通過驗證";
                TempData["isLoginEmailPop"] = true;
                return Redirect($"{nowUrl}");
            }

            var memberInfo = await _accountManager.GetMemberInfoByAccountInfoAsync(personalAccountInfo);

            await _accountManager.LoginAsync(memberInfo, inputLoginVM.IsRemember);

            return Redirect($"{returnUrl}");
        }





        public async Task<IActionResult> LineSSOCallBack()
        {
            var nowUrl = getNowUrl() ?? "/";
            var returnUrl = getReturnUrl() ?? nowUrl;


            //取得返回的code
            var code = HttpContext.Request.Query["code"].ToString();
            if (string.IsNullOrEmpty(code))
            {
                using (var sw = new System.IO.StreamWriter(HttpContext.Response.Body, System.Text.Encoding.UTF8))
                {
                    sw.Write("沒有正確回應code");
                    sw.Flush();
                    return new OkResult();
                }
            }

            //顯示，測試用
            //從Code取回toke
            var token = isRock.LineLoginV21.Utility.GetTokenFromCode(code,
                _lineChannel_ID.Value,  //TODO:請更正為你自己的 client_id
               _lineChannel_Secret.Value, //TODO:請更正為你自己的 client_secret
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{_lineCallBackURL.Value}");  //TODO:請檢查此網址必須與你的LINE Login後台Call back URL相同


            //利用access_token取得用戶資料
            var user = isRock.LineLoginV21.Utility.GetUserProfile(token.access_token);

            //_accountManager.GetAccountAsync();





            //利用id_token取得Claim資料
            var JwtSecurityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token.id_token);
            var email = "";
            //如果有email
            if (JwtSecurityToken.Claims.ToList().Find(c => c.Type == "email") != null)
                email = JwtSecurityToken.Claims.First(c => c.Type == "email").Value;

            Member memberFromDb;


            // 找 User 有沒有註冊過，在 LineRegister 找有沒有 lineUserId 有沒有重複的
            var lineRegisterFromDb = await _accountManager.GetLineInfoByLineUserIdFromDb(user.userId);

            if (lineRegisterFromDb is null)
            {
                // 沒有重複的要建立會員資料和Line 資料  
                memberFromDb = await _accountManager.CreateMemberInfoByLine(email, user.displayName, user.pictureUrl);

                lineRegisterFromDb = await _accountManager.CreateLineUserInfo(user.userId, memberFromDb.MemberId);

            }
            else
            {
                memberFromDb = await _accountManager.GetMemberByMemberId(lineRegisterFromDb.MemberId);
            }

            // 有重複地進入登入程序

            await _accountManager.LoginAsync(memberFromDb, true);


            return Redirect($"{returnUrl}");

        }


        public async Task<IActionResult> GoogleSSOCallBack()
        {
            var nowUrl = getNowUrl() ?? "/";
            var returnUrl = getReturnUrl() ?? nowUrl;

            string code = Request.Query["code"];
            if (string.IsNullOrEmpty(code))
            {
                using (var sw = new System.IO.StreamWriter(HttpContext.Response.Body, System.Text.Encoding.UTF8))
                {
                    sw.Write("沒有正確回應code");
                    sw.Flush();
                    return new OkResult();
                }
            }

            string client_id = _googleClient_ID.Value;
            //todo: replace it with google SSO client_secret 
            string client_secret = _googleClient_Secret.Value;
            //todo: replace it with google SSO redirecr_url 
            string redirecr_url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{_googleCallBackURL.Value}";

            //get token
            var token = isRock.Toolbox.GoogleSSO.Helper.GetTokenFromCode(code, client_id, client_secret, redirecr_url);
            //get User Info
            var userinfo = isRock.Toolbox.GoogleSSO.Helper.GetUserInfo(token.access_token);
            //SignIn with Cookie Authentication




            Member memberFromDb;


            // 找 User 有沒有註冊過，在 LineRegister 找有沒有 lineUserId 有沒有重複的
            var googleRegisterFromDb = await _accountManager.GetGoogleInfoByGoogleUserIdFromDb(userinfo.id);

            if (googleRegisterFromDb is null)
            {
                // 沒有重複的要建立會員資料和Line 資料  
                memberFromDb = await _accountManager.CreateMemberInfoByGoogle(userinfo.email, userinfo.name, userinfo.picture);

                googleRegisterFromDb = await _accountManager.CreateGoogleUserInfo(userinfo.id, memberFromDb.MemberId);

            }
            else
            {
                memberFromDb = await _accountManager.GetMemberByMemberId(googleRegisterFromDb.MemberId);
            }

            // 有重複地進入登入程序

            await _accountManager.LoginAsync(memberFromDb, true);


            return Redirect($"{returnUrl}");

        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountManager.SignOutAsync();
            TempData["isLoginPop"] = false;

            return Redirect("/");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([FromForm] SignUpViewModel input)
        {
            var nowUrl = getNowUrl() ?? "/";
            var returnUrl = getReturnUrl() ?? nowUrl;

            if (!ModelState.IsValid)
            {
                TempData["isSignUpError"] = true;
                TempData["isSignUpPop"] = true;
                TempData["SignUpErrorMsg"] = "帳號或密碼格式錯誤，密碼 6-12 字元且至少要有一個數字與英文";

                return Redirect($"{nowUrl}");
            }

            if (input.TermsOfUse == false)
            {
                TempData["isSignUpError"] = true;
                TempData["isSignUpPop"] = true;
                TempData["SignUpErrorMsg"] = "請詳閱使用者條款並確認";
                return Redirect($"{nowUrl}");
            }

            var accountInfo = new AccountInfo()
            {
                Account = input.Email,
                Password = input.Password,
            };



            if (_accountManager.IsExistUser(accountInfo))
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "此帳號已被使用";
                return Redirect($"{nowUrl}");
            }


            var accountInfoFromDb = await _accountManager.SignUpAsync(input);



            // 寄信流程     

            var isSendEmailStatusCode = await _accountManager.SendRegisterEmailAsync(accountInfoFromDb);

            if (!isSendEmailStatusCode)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "信件寄送錯誤";

                return Redirect($"{nowUrl}");
            }

            TempData["isAccountError"] = true;
            TempData["isAccountMsgPop"] = true;
            TempData["isAccountMsg"] = "認證信已寄出，請至信箱確認";


            return Redirect($"{nowUrl}");
        }


        [HttpGet]
        public async Task<IActionResult> Register(int accountInfoId, string intputExpirationTime)
        {
            var expirationTimeStringArray = intputExpirationTime.Split('-');

            if (expirationTimeStringArray.Length != 6)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "網址格式錯誤";
                return Redirect("/");
            }

            var isConvert = expirationTimeStringArray.All(x => int.TryParse(x, out int a));

            if (!isConvert)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "網址格式錯誤";
                return Redirect("/");
            }

            var expirationTimeIntArray = expirationTimeStringArray.Select(x => int.Parse(x)).ToList();

            var expirationTime = new DateTime(expirationTimeIntArray[0], expirationTimeIntArray[1], expirationTimeIntArray[2], expirationTimeIntArray[3], expirationTimeIntArray[4], expirationTimeIntArray[5]);

            // 檢查信箱是否已經驗證過了
            AccountInfo accountInfo = await _accountManager.GetAccountInfoByIdAsync(accountInfoId);

            if (accountInfo.Verify)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "您的帳號已驗證";
                return Redirect("/");
            }

            if (DateTime.Compare(expirationTime, DateTime.UtcNow) < 0)
            {
                var updateAccount = _accountManager.UpdateExpirationTime(accountInfoId);
                var isSendEmailStatusCode = await _accountManager.SendRegisterEmailAsync(updateAccount);
                if (!isSendEmailStatusCode)
                {
                    TempData["isAccountError"] = true;
                    TempData["isAccountMsgPop"] = true;
                    TempData["isAccountMsg"] = "信件寄送錯誤";
                    return Redirect("/");
                }

                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "已逾期，已重新寄送電子郵件，請至信箱確認";

                // 做一個逾期頁面
                return Redirect("/");
            }

            if (!_registerViewModelService.ConfirmData(accountInfoId, intputExpirationTime))
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "資料錯誤";
                return Redirect("/");
            }



            var viewModel = new RegisterViewModel()
            {
                CountyList = _registerViewModelService.GetCounty(),
                AccountInfoId = accountInfoId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                registerViewModel.CountyList = _registerViewModelService.GetCounty();
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "資料檢核錯誤";
                return View(registerViewModel);
            }
            //var a = JsonSerializer.Deserialize<List<CountyDto>>(registerViewModel.JsonText);

            // check 是否已經 MemberInfo 檢核
            // 檢查信箱是否已經驗證過了
            AccountInfo accountInfo = await _accountManager.GetAccountInfoByIdAsync(registerViewModel.AccountInfoId);

            if (accountInfo.Verify)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "您的帳號已驗證，請登入。";
                return Redirect("/");
            }


            var member = await _accountManager.RegisterMember(registerViewModel);

            var sendRegisterOkEmail = new EmailContentDTO
            {
                UserEmail = member.Email,
                UserName = member.Name,
            };

            await _sendGridService.RegisterOkEmail(sendRegisterOkEmail);


            await _accountManager.LoginAsync(member, true);



            return Redirect("/");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotViewModel input)
        {
            var nowUrl = getNowUrl() ?? "/";
            var returnUrl = getReturnUrl() ?? nowUrl;

            if (!ModelState.IsValid)
            {
                TempData["ForgotPassWordErrorMsg"] = "請輸入正確的帳號格式";
                TempData["isForgotPop"] = true;
                TempData["isForgotError"] = true;

                return Redirect($"{nowUrl}");
            }



            var accountInfo = new AccountInfo()
            {
                Account = input.Email,
            };

            if (!_accountManager.IsExistUser(accountInfo))
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "此帳號不存在";

                return Redirect($"{nowUrl}");


            }

            var userAccountInfo = await _accountManager.GetAccountInfoByEmailAsync(input.Email);

            if (!userAccountInfo.Verify)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "您的帳號未驗證";

                return Redirect($"{nowUrl}");
            }

            // 更新 ExpirationTime

            var updateUserAccountInfo = _accountManager.UpdateExpirationTime(userAccountInfo.AccountInfoId);


            // "密碼重設發送 E-mail"
            // "請檢查您的電子郵件，然後單擊確認連結重新設定您的密碼!"

            // 寄信流程



            var isSendEmailStatus = await _accountManager.SendForgotPasswordEmailAsync(updateUserAccountInfo);

            if (!isSendEmailStatus)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "寄信失敗，請再重試一次";

                return Redirect($"{nowUrl}");
            }

            //var isSendEmailStatusCode = await sendEmailAsync(accountInfoFromDb);

            //if (!isSendEmailStatusCode)
            //{
            //    ViewBag.SignUpError = "信件寄送錯誤";
            //    ViewBag.isSignUp = true;
            //    return Redirect("/");
            //}

            TempData["isAccountError"] = true;
            TempData["isAccountMsgPop"] = true;
            TempData["isAccountMsg"] = "請至信箱確認";


            return Redirect($"{nowUrl}");
        }

        //{accountInfoId}/{intputExpirationTime}

        [HttpGet]
        public IActionResult ForgotPassword(int accountInfoId, string intputExpirationTime)
        {
            var nowUrl = getNowUrl() ?? "/";
            var returnUrl = getReturnUrl() ?? nowUrl;

            var expirationTimeStringArray = intputExpirationTime.Split('-');

            if (expirationTimeStringArray.Length != 6)
            {

                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "網址格式錯誤";
                return Redirect($"{nowUrl}");
            }

            var isConvert = expirationTimeStringArray.All(x => int.TryParse(x, out int a));

            if (!isConvert)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "網址格式錯誤";
                return Redirect($"{nowUrl}");

            }

            var expirationTimeIntArray = expirationTimeStringArray.Select(x => int.Parse(x)).ToList();

            var expirationTime = new DateTime(expirationTimeIntArray[0], expirationTimeIntArray[1], expirationTimeIntArray[2], expirationTimeIntArray[3], expirationTimeIntArray[4], expirationTimeIntArray[5]);



            if (DateTime.Compare(expirationTime, DateTime.UtcNow) < 0)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "信件已逾期";

                // 做一個逾期頁面
                return Redirect($"{nowUrl}");

            }

            if (!_registerViewModelService.ConfirmData(accountInfoId, intputExpirationTime))
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "資料錯誤";
                return Redirect($"{nowUrl}");

            }
            var resetPasswordVM = new ResetPasswordViewModel
            {
                UpdateAccount = new UpdateAccountByResetPassword { IsUpdate = false },
                IntputExpirationTime = intputExpirationTime,
                AccountInfoId = accountInfoId
            };
            return View(resetPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("/Account/ResetPassword", Name = "resetpassword")]
        public IActionResult ResetPassword([FromForm] ResetPasswordViewModel resetPassword)
        {
            if (resetPassword.NewPassword != resetPassword.NewCheckPassword)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "輸入兩個密碼不相符";
                return Redirect("/");
            }

            var expirationTimeStringArray = resetPassword.IntputExpirationTime.Split('-');

            if (expirationTimeStringArray.Length != 6)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "網址格式錯誤";
                return Redirect("/");

            }

            var isConvert = expirationTimeStringArray.All(x => int.TryParse(x, out int a));

            if (!isConvert)
            {
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "網址格式錯誤";
                return Redirect("/");

            }

            var expirationTimeIntArray = expirationTimeStringArray.Select(x => int.Parse(x)).ToList();

            var expirationTime = new DateTime(expirationTimeIntArray[0], expirationTimeIntArray[1], expirationTimeIntArray[2], expirationTimeIntArray[3], expirationTimeIntArray[4], expirationTimeIntArray[5]);


            if (DateTime.Compare(expirationTime, DateTime.UtcNow) < 0)
            {
                //var updateAccount = _accountManager.UpdateExpirationTime(accountInfoId);
                //var isSendEmailStatusCode = await _accountManager.SendRegisterEmailAsync(updateAccount);
                //if (!isSendEmailStatusCode)
                //{
                //    ViewBag.RegisterError = "信件寄送錯誤";
                //    return Redirect("/");
                //}
                TempData["isAccountError"] = true;
                TempData["isAccountMsgPop"] = true;
                TempData["isAccountMsg"] = "信件已逾期";
                return Redirect("/");


            }



            var returnResetPasswordVM = _accountManager.UpdatePasswordByForgotPassword(resetPassword);

            TempData["isAccountError"] = true;
            TempData["isAccountMsgPop"] = true;
            TempData["isAccountMsg"] = "密碼更新完畢，請重新登入";
            return Redirect("/");

        }




        // 未授權
        public IActionResult AccessDenied()
        {
            return View();
        }

        private string getNowUrl()
        {
            return Request.Cookies["thisIsNowUrl"];
        }

        private string getReturnUrl()
        {
            return Request.Cookies["returnUrl"];
        }

    }



}

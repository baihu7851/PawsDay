using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.Account;
using PawsDayBackEnd.Helpers;
using PawsDayBackEnd.Services;
using SendGrid;
using System;

namespace PawsDayBackEnd.WebApi
{

    public class AuthApiController : BaseApiController
    {
        private readonly JwtHelper _jwt;
        private readonly AccountServices _accountService;
        private readonly BlockTokenServices _blockTokenServices;

        public AuthApiController(JwtHelper jwt, AccountServices accountService, BlockTokenServices blockTokenServices)
        {
            _jwt = jwt;
            _accountService = accountService;
            _blockTokenServices = blockTokenServices;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO request)
        {
            var response = new ApiResultDto();
            //檢核身分
            if (_accountService.VerifyAccount(request) != VerifyResponse.VerifySuccess)
            {
                response.Message = "驗證失敗";
                return Ok(response);
            }

            var token = _jwt.GenerateToken(request.UserName);
            response.Status = DTO.StatusCode.Success;
            response.Data = token;
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = AuthorizationConstants.Administrator)]
        public IActionResult GetUserInfo()
        {
            var user = _accountService.GetUserInfo(User.Identity.Name);
            var response = new ApiResultDto(user);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationConstants.Administrator)]
        public IActionResult Logout([FromBody] LogoutDto request)
        {
            //登出時把token加入黑名單
            _blockTokenServices.AddBlockToken(request);
            return Ok();
        }

    }
}

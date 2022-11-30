using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace PawsDayBackEnd.Filters
{
    public class AdminAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly IRepository<BlockToken> _blockToken;

        public AdminAuthorize(IRepository<BlockToken> blockToken)
        {
            _blockToken = blockToken;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //檢核action是否有帶AllowAnonymousAttribute，如果任何人都可訪問直接跳出
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            //判斷驗整&授權
            if (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                //如果驗證有效，檢核token是否已失效(被加入block)
                var authorization = context.HttpContext.Request.Headers["Authorization"];
                
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var parameter = headerValue.Parameter;
                    if (!_blockToken.Any(x => x.Token.ToLower() == parameter.ToLower()))
                    {
                        return;
                    }
                }
            }
            //如果驗證無效直接跳回401
            context.Result = new UnauthorizedResult();
        }
    }
}

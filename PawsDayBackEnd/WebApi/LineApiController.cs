using ApplicationCore.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.LineBot;
using PawsDayBackEnd.Services;
using System.Threading.Tasks;

namespace PawsDayBackEnd.WebApi
{
    [Authorize(Roles = AuthorizationConstants.Administrator)]
    public class LineApiController : BaseApiController
    {
        private readonly LineBotService _service;
        public LineApiController(LineBotService service)
        {
             _service = service;
        }

        [HttpGet]
        public ApiResultDto GetKeyWord()
        {
            var response = _service.GetKeyWord();
            return response;

        }

        [HttpPost]
        public ApiResultDto CreateKeyWord(KeyWordDto request)
        {
            var response = _service.CreateKeyWord(request.keyword, request.action);
            return response;
        }

        [HttpPost]
        public ApiResultDto UpdateKeyWord(KeyWordDto request)
        {
            var response = _service.UpdateKeyWord(request.id, request.keyword, request.action);
            return response;
        }

        [HttpPost]
        public ApiResultDto DeleteKeyWord(int keywordid)
        {
            var response = _service.DeleteKeyWord(keywordid);
            return response;
        }

        [HttpGet]
        public ApiResultDto GetTemplate(int templateid)
        {
            var response = _service.GetTemplate(templateid);
            return response;
        }

        [HttpPost]
        public async Task<ApiResultDto> UpdateTemplate(int templateid, TemplateDto request)
        {
            var response = await _service.UpdateTemplate(templateid, request);
            return response;
        }
    }
}

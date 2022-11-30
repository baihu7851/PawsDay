using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDayBackEnd.DTO;
using PawsDayBackEnd.DTO.LineBot;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PawsDayBackEnd.Services
{
    public class LineBotService
    {
        private readonly IRepository<LineBotKeyWord> _keyword;
        private readonly IRepository<LineBotTemplate> _template;
        private readonly IRepository<LineBotTemplateDetail> _templatedetail;
        private readonly ITemplateRepository _repository;

        public LineBotService(
            IRepository<LineBotKeyWord> keyword,
            IRepository<LineBotTemplate> template,
            IRepository<LineBotTemplateDetail> templatedetail,
            ITemplateRepository repository
            )
        {
            _keyword = keyword;
            _template = template;
            _templatedetail = templatedetail;
            _repository = repository;
        }

        public ApiResultDto GetKeyWord()
        {
            var response = _keyword.GetAllReadOnly().Select(k=>new KeyWordDto { id=k.KeyWordId,keyword=k.KeyWord,action=k.Action,canbeedit=k.CanBeEdit}).ToList();
            return new ApiResultDto(response);
        }

        public ApiResultDto CreateKeyWord(string keyword, string action)
        {
            var target = new LineBotKeyWord() 
            {
                KeyWord=keyword,
                Action=action,
                CanBeEdit=true              
            };

            var response = new ApiResultDto();
            try
            {
                _keyword.Add(target);
                response.Status = StatusCode.Success;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"新增失敗{ex}";
            }
            return response;
        }
        public ApiResultDto UpdateKeyWord(int keywordid,string keyword,string action)
        {
            var target = _keyword.GetById(keywordid);
            target.KeyWord = keyword;
            target.Action = action;

            var response = new ApiResultDto();
            try
            {
                _keyword.Update(target);
                response.Status = StatusCode.Success;
            }
            catch(Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗{ex}";
            }
            return response;
        }
        public ApiResultDto DeleteKeyWord(int keywordid)
        {
            var target = _keyword.GetById(keywordid);

            var response = new ApiResultDto();
            try
            {
                _keyword.Delete(target);
                response.Status = StatusCode.Success;
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗{ex}";
            }
            return response;
        }


        public ApiResultDto GetTemplate(int templateid)
        {
            var template = _template.GetById(templateid);
            var detail = _templatedetail.GetAllReadOnly().Where(d => d.Template == templateid).ToList();

            var data = new TemplateDto
            {
                title = template.Title,
                text = template.Text,
                image = template.ImageUrl,
                detail = detail.Select(d => new DetailDto {text=d.Text,type=d.Type,url=d.Url }).ToList()
            };

            return new ApiResultDto(data);
        }

        public async Task<ApiResultDto> UpdateTemplate(int templateid,TemplateDto lineBotTemplateDto)
        {
            var target = _template.GetById(templateid);

            target.Title= lineBotTemplateDto.title;
            target.Text = lineBotTemplateDto.text;
            //target.Time = templateid==1? lineBotTemplateDto.time : DateTime.UtcNow ;
            target.Time =  DateTime.UtcNow;
            target.ImageUrl = lineBotTemplateDto.image;

            var newdetail = lineBotTemplateDto.detail.Select(d => new LineBotTemplateDetail 
            {
                Template=templateid,
                Text=d.text,
                Type=d.type is null? "":d.type,
                Url=d.url is null? "":d.url
            }).ToList();

            var response = new ApiResultDto();
            try
            {
                var result = await _repository.UpdateTemplate(target, newdetail);
                if (result) { response.Status = StatusCode.Success; }
                else { response.Status = StatusCode.Failed; }
                return response;                
            }
            catch (Exception ex)
            {
                response.Status = StatusCode.Failed;
                response.Message = $"儲存失敗:{ex.Message}";
            }

            return response;
        }

    
    
    
    }
}

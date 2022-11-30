using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using PawsDay.WebApi.LineBot.Dto;
using System;
using System.Linq;

namespace PawsDay.Services.LineBot
{
    public class LineBotService
    {
        private readonly IRepository<LineBotKeyWord> _keyword;
        private readonly IRepository<LineBotTemplate> _template;
        private readonly IRepository<LineBotTemplateDetail> _templatedetail;
        private readonly IRepository<LineBotHistory> _history;

        public LineBotService(
            IRepository<LineBotKeyWord> keyword,
            IRepository<LineBotTemplate> template,
            IRepository<LineBotTemplateDetail> templatedetail,
            IRepository<LineBotHistory> history
            )
        {
            _keyword = keyword;
            _template = template;
            _templatedetail = templatedetail;
            _history = history;
        }


        //text進來，要有方法解析action (查keyword表)
        public string SearchForKeyWord(string input)
        {
            var result = _keyword.GetAllReadOnly().FirstOrDefault(k => input.Contains(k.KeyWord));
            if (result != null) { return result.Action; }

            return "default";
        }

        //取得模板資訊
        public LineBotTemplateDto GetTemplate(int templateid)
        {
            var template = _template.GetById(templateid);
            var detail = _templatedetail.GetAllReadOnly().Where(d => d.Template == templateid).ToList();

            var response = new LineBotTemplateDto
            {
                Title = template.Title,
                Text = template.Text,
                Image = template.ImageUrl,
                Time = template.Time,
                Detail = detail.Select(d => new TemplateDetailDto
                {
                    DetailText = d.Text,
                    Type = d.Type is null? "" : d.Type,
                    Url = d.Url is null ? "" : d.Url
                }).ToList()
            };

            return response;
        }

        //加入歷史訊息
        public void CreateHistory(string userid, string text)
        {
            var history = new LineBotHistory
            {
                UserId = userid,
                Text = text,
                CreateTime = DateTime.UtcNow
            };

            _history.Add(history);
        }

        //查詢歷史訊息
        public string CheckHistory(string userid)
        {
            var now = DateTime.UtcNow;
            var history = _history.GetAllReadOnly().Where(h => h.UserId == userid && h.CreateTime.AddMinutes(1) >= now && h.CreateTime<= now).OrderByDescending(h=>h.CreateTime);
            if (history.Count()!=0) { return history.First().Text; }
            return "";
        }
    }
}

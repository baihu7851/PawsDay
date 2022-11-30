using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace PawsDay.WebApi.LineBot.Dto
{
    public class LineBotTemplateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime Time { get; set; }
        public List<TemplateDetailDto> Detail { get; set; }

    }
}

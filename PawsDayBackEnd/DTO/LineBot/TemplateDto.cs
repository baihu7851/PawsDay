using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System;

namespace PawsDayBackEnd.DTO.LineBot
{
    public class TemplateDto
    {
        public string title { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public DateTime time { get; set; }
        public List<DetailDto> detail { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class LineBotTemplateDetail :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateDetailId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public string Url { get; set; }
        public int Template { get; set; }

        public virtual LineBotTemplate LineBotTemplate { get; set; }
    }
}

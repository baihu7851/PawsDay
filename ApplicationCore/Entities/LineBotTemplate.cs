using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class LineBotTemplate : BaseEntity
    {
        public LineBotTemplate()
        {
            LineBotTemplateDetails = new HashSet<LineBotTemplateDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemplateId { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Time { get; set; }

        public virtual ICollection<LineBotTemplateDetail> LineBotTemplateDetails { get; set; }

    }
}

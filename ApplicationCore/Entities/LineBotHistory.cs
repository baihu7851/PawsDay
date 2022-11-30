using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class LineBotHistory:BaseEntity
    {
        public int HistoryId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

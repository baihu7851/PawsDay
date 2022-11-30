using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class LineBotKeyWord : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KeyWordId{get;set;}
        public string KeyWord{get;set;}
        public string Action { get; set; }
        public bool CanBeEdit { get; set; }
    }
}

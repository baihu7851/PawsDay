using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class License: BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LicenseId { get; set; }
        public string LicenseUrl { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
    }
}

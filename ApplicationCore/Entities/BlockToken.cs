using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class BlockToken : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BlockTokenId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpireTime { get; set; }
    }
}

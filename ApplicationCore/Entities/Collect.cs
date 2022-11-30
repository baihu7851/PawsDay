using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Collect : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectId { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}

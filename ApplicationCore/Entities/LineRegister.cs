using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class LineRegister : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineRegisterId { get; set; }
        public string Account { get; set; }

        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

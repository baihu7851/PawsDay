using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class GoogleRegister : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GoogleRegisterId { get; set; }
        public string Account { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

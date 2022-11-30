using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class CartSchedule : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartScheduleId { get; set; }
        public int CartId { get; set; }
        public DateTime ServiceDate { get; set; }
        public int Schedule { get; set; }

        public virtual Cart Cart { get; set; }
    }
}

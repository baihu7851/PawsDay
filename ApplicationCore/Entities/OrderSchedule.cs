using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class OrderSchedule : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderScheduleId { get; set; }
        public int OrderId { get; set; }
        public DateTime ServiceDate { get; set; }
        public int Schedule { get; set; }

        public virtual Order Order { get; set; }
        public virtual Schedule ScheduleNavigation { get; set; }
    }
}

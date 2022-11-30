using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Schedule : BaseEntity
    {
        public Schedule()
        {
            OrderSchedules = new HashSet<OrderSchedule>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }
        public string TimeDesrcipt { get; set; }
        public int PartTimeId { get; set; }

        public virtual ICollection<OrderSchedule> OrderSchedules { get; set; }
    }
}

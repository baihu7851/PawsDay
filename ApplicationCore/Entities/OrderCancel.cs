using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class OrderCancel : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderCancelId { get; set; }
        public int OrderId { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
        public decimal RefundPersent { get; set; }

        public virtual Order Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class ProductServiceTime : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductServiceTimeId { get; set; }
        public int ProductId { get; set; }
        public int ServiceDay { get; set; }
        public int ServicePartTime { get; set; }

        public virtual Product Product { get; set; }
    }
}

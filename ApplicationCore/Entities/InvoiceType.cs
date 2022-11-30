using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class InvoiceType : BaseEntity
    {
        public InvoiceType()
        {
            Orders = new HashSet<Order>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceTypeId { get; set; } 
        public string TypeName { get; set; } //開立方式

        public virtual ICollection<Order> Orders { get; set; }
    }
}

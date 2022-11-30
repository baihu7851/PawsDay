using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class ProductDiscount : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductDiscountId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public virtual Product Product { get; set; }
    }
}

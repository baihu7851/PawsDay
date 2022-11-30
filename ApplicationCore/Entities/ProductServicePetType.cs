using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class ProductServicePetType : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductServicePetTypeId { get; set; }
        public int ProductId { get; set; }
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public decimal Price { get; set; }
        public decimal OvernightPrice { get; set; }

        public virtual Product Product { get; set; }
    }
}

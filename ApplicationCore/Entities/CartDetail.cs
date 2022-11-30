using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class CartDetail : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int PetType { get; set; }
        public int ShapeType { get; set; }

        public virtual Cart Cart { get; set; }
    }
}

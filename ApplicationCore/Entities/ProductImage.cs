using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class ProductImage : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImagesId { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public int Sort { get; set; }

        public virtual Product Product { get; set; }
    }
}

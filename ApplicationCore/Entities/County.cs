using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class County : BaseEntity
    {
        public County()
        {
            Carts = new HashSet<Cart>();
            Members = new HashSet<Member>();
            ProductServiceAreas = new HashSet<ProductServiceArea>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountyId { get; set; }
        public string CountyName { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<ProductServiceArea> ProductServiceAreas { get; set; }
    }
}

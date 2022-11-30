using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class District : BaseEntity
    {
        public District()
        {
            Carts = new HashSet<Cart>();
            Members = new HashSet<Member>();
            ProductServiceAreas = new HashSet<ProductServiceArea>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int CountyId { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<ProductServiceArea> ProductServiceAreas { get; set; }
    }
}

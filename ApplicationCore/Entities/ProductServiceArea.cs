using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class ProductServiceArea : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductServiceAreaId { get; set; }
        public int ProductId { get; set; }
        public int County { get; set; }
        public int District { get; set; }

        public virtual County CountyNavigation { get; set; }
        public virtual District DistrictNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}

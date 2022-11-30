using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class ServiceType : BaseEntity
    {
        public ServiceType()
        {
            Products = new HashSet<Product>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceTypeId { get; set; }
        public string TypeName { get; set; }
        public string ServiceIntro { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

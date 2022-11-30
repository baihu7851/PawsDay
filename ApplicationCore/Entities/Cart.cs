using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Cart : BaseEntity
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
            CartSchedules = new HashSet<CartSchedule>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreateTime { get; set; }
        public int County { get; set; }
        public int District { get; set; }

        public virtual County CountyNavigation { get; set; }
        public virtual Member Customer { get; set; }
        public virtual District DistrictNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<CartSchedule> CartSchedules { get; set; }
    }
}

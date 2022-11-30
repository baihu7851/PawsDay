using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Product:BaseEntity
    {
        public Product()
        {
            AdProject = new HashSet<AdProject>();
            Carts = new HashSet<Cart>();
            Collects = new HashSet<Collect>();
            ProductDiscounts = new HashSet<ProductDiscount>();
            ProductImages = new HashSet<ProductImage>();
            ProductServiceAreas = new HashSet<ProductServiceArea>();
            ProductServicePetTypes = new HashSet<ProductServicePetType>();
            ProductServiceTimes = new HashSet<ProductServiceTime>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public int ServiceType { get; set; }
        public string Introduce { get; set; }
        public int SitterId { get; set; } //會員編號
        public int ProductStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public bool IsDelete { get; set; }

        public virtual ServiceType ServiceTypeNavigation { get; set; }
        public virtual Member Sitter { get; set; }
        public virtual ICollection<AdProject> AdProject { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Collect> Collects { get; set; }
        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductServiceArea> ProductServiceAreas { get; set; }
        public virtual ICollection<ProductServicePetType> ProductServicePetTypes { get; set; }
        public virtual ICollection<ProductServiceTime> ProductServiceTimes { get; set; }
    }
}

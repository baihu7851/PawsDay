using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class OrderPetDetail : BaseEntity
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderPetId { get; set; }
        public string PetName { get; set; }
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public bool PetSex { get; set; }
        public int BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public bool Ligation { get; set; }
        public bool Vaccine { get; set; }
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int ServiceCount { get; set; }

        public string PetDiscription { get; set; } 
        public string PetIntro { get; set; }
        public virtual Order Order { get; set; }

    }
}

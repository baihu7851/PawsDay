using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class PetInfomation : BaseEntity
    {
        public PetInfomation()
        {
            PetDispositions = new HashSet<PetDisposition>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PetId { get; set; }
        public int MemberId { get; set; }
        public string PetName { get; set; }
        public int PetType { get; set; }
        public int ShapeType { get; set; }
        public bool PetSex { get; set; }
        public int BirthYear { get; set; }
        public int? BirthMonth { get; set; }
        public bool Ligation { get; set; }
        public bool Vaccine { get; set; }
        public string Intro { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? EditTime { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<PetDisposition> PetDispositions { get; set; }
    }
}

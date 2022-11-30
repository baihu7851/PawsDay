using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Disposition : BaseEntity
    {
        public Disposition()
        {
            PetDispositions = new HashSet<PetDisposition>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DispositionId { get; set; }
        public string DispositionType { get; set; }

        public virtual ICollection<PetDisposition> PetDispositions { get; set; }
    }
}

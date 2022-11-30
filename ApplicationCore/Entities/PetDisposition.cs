using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class PetDisposition : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PetDispositionId { get; set; }
        public int DispositionType { get; set; }
        public int PetId { get; set; }

        public virtual Disposition DispositionTypeNavigation { get; set; }
        public virtual PetInfomation Pet { get; set; }
    }
}

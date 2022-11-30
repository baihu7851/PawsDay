using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Aptitude : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AptitudeId { get; set; }
        public int MemberId { get; set; }
        public int AptitudeExtrovert { get; set; }
        public int AptitudeIntrovert { get; set; }
        public int AptitudeThinking { get; set; }
        public int AptitudeFeeling { get; set; }

        public virtual Member Member { get; set; }
    }
}

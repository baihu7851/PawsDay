using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class RegisterType : BaseEntity
    {
        public RegisterType()
        {
            Members = new HashSet<Member>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

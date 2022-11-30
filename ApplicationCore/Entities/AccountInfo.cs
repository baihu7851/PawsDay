using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class AccountInfo:BaseEntity
    {
        public AccountInfo()
        {
            Members = new HashSet<Member>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountInfoId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool Verify { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

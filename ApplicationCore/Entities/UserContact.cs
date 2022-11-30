using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class UserContact : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserContactId { get; set; }
        public int MemberId { get; set; }
        public int SitterId { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }

        public bool IsMemberSpeak { get; set; }

        public virtual Member Member { get; set; }
        public virtual Member Sitter { get; set; }
    }
}

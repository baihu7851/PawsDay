using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class OfficialContact : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfficialContactId { get; set; }

        public int? OrderId { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsUserSpeak { get; set; }

        public virtual Member User { get; set; }
        public virtual Order Order { get; set; }
    }
}

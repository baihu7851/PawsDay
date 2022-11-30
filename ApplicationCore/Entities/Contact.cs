using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Contact : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string ContactContent { get; set; }
        public bool Status { get; set; }
        public string ReplyContent { get; set; }
        public DateTime? ReplyTime { get; set; }
    }
}

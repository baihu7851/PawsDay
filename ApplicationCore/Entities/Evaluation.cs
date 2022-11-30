using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Evaluation : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvaluationId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public int EvaluationScore { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual Order Order { get; set; }
        public virtual Member User { get; set; }
    }
}

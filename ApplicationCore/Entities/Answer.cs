using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class Answer : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }
        public int MemberId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerInput { get; set; }

        public Member Member { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApplicationCore.Entities
{
    public partial class RegisterSitter : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SitterId { get; set; }
        public int MemberId { get; set; }
        public string Id { get; set; }
        public string Idimagefont { get; set; }
        public string Idimageback { get; set; }
        /// <summary>
        /// 另存
        /// 簡答題3題
        /// 性向測驗
        /// 上傳證照
        /// </summary>
        public string PetExperience { get; set; }
        public int Score { get; set; }
        public int RegisterStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? VerifyTime { get; set; }
        public string SitterName { get; set; }
        public string SitterPicture { get; set; }
        public string SitterInfo { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public DateTime? EditTime { get; set; }

        public virtual Member Member { get; set; }
    }
}

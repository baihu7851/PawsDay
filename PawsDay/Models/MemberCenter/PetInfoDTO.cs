using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PawsDay.Models.MemberCenter
{
    public class PetInfoDTO
    {
        public int PetID { get; set; }

        public string PetName { get; set; } //名稱

        public string PetGender { get; set; } //性別
       
        public string PetType { get; set; } //類別
        
        public string ShapeType { get; set; } //體型
        
        public string BirthYear { get; set; } //出生年
        
        public string? BirthMonth { get; set; } //出生月
        
        public List<string> PetDispositionType { get; set; } //個性名稱
        
        public bool Ligation { get; set; } //結紮
        
        public bool Vaccine { get; set; } //疫苗
        
        public string Description { get; set; } //描述

    }
}

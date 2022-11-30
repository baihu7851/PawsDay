using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Common
{
    public enum UserType
    {
        [Display(Name = "訪客")]
        Normal = 0,
        [Display(Name = "會員")]
        Member = 1,
        [Display(Name = "保姆")]
        Sitter = 2,
        [Display(Name = "管理者")]
        Admin = 3
    }
    public enum PetType
    {
        [Display(Name ="狗狗")]
        Dog = 0,
        [Display(Name ="貓咪")]
        Cat = 1
    }
    public enum ShapeType
    {
        [Display(Name = "迷你型(5kg以下)")]
        Mini = 0,
        [Display(Name = "小型(5~10kg以下)")]
        Small = 1,
        [Display(Name = "中型(10~20kg以下)")]
        Middle = 2,
        [Display(Name = "大型(20~40kg以下)")]
        Large = 3,
        [Display(Name = "超大型(20kg以上)")]
        Huge = 4
    }
    public enum ServieDay
    {
        Mondy = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 0
    }
    public enum ServiceTime
    {
        [Display(Name = "00:00~05:59")]
        Aftermidnight = 0,
        [Display(Name = "06:00~11:59")]
        Morning = 1,
        [Display(Name = "12:00~17:59")]
        Afternoon = 2,
        [Display(Name = "18:00~23:59")]
        Night = 3
    }

    public enum AptitudeType 
    {
        Quiz = 0,
        [Display(Name = "E外向")]
        Extrovert = 1,
        [Display(Name = "I內向")]
        Introvert = 2,
        [Display(Name = "T理性")]
        Thinking=3,
        [Display(Name = "F感性")]
        Feeling= 4
    }

    public enum ProductStatus
    {
        [Display(Name = "上架中")]
        OnSale= 0,
        [Display(Name = "下架中")]
        OffSale = 1
    }

    public enum SitterStatus
    {
        [Display(Name = "尚未審核")]
        AwaitToCheck=0,
        [Display(Name = "審核通過")]
        Approved =1,
        [Display(Name = "審核未通過")]
        Reject =2,
        [Display(Name = "停權")]
        Suspend = 3
    }
    public enum OrderStatus
    {
        
        [Display(Name = "訂單成立")]
        Success =0,
        [Display(Name = "訂單取消")]
        Cancel = 1,
        [Display(Name ="訂單完成")]
        Complete=2,
        [Display(Name ="訂單處理中")]
        Handle=3

    }

    public enum RegisterTypes
    {
        [Display(Name = "訪客")]
        Normal =0,
        [Display(Name = "PawsDay")]
        Email = 1,
        [Display(Name = "Line")]
        Line = 2,
        [Display(Name = "Google")]
        Google = 3,
        [Display(Name = "Facebook")]
        Facebook = 4
    }
}

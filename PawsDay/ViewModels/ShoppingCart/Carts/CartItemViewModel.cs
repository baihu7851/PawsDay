using Microsoft.AspNetCore.Mvc.Rendering;
using PawsDay.ViewModels.ShoppingCart.Carts.CartsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawsDay.ViewModels.ShoppingCart.Carts
{
    public class CartItemViewModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        //服務照片(FK)
        public string Photo { get; set; }
       

        //保姆名稱&服務類型(FK)
        public string SitterName { get; set; }
        public string Service { get; set; }

        //服務日期 & 時間(FK)
        public string ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public int TimeSpan { get; set; }
        public string ScheduleIdsStr { get; set; } //唯給訪客計算寵物單價用


        public int NumberOfPets { get; set; }
        public List<PetTitleDTO> PetListHeader { get; set; } 
        public string ShapeType { get; set; }
        public CreateSelectedDTO SelectedTypeOptions { get; set; }

        //服務地址如下
        public int CartCity { get; set; }
        public string CartCityName { get; set; }
        public int CartDistrict { get; set; }
        public string CartDistrictName { get; set; }
        public string CartAddress { get; set; }
      

        //寵物資訊如下

        public List<PetInfoViewModel> PetFullInfoList { get; set; } //寵物詳細資料

        //托保期間聯絡方式
        //[Required(ErrorMessage = "請填寫聯絡人全名")]
        public string ContactPersonName { get; set; }

        public string ContactPersonTel { get; set; }

        public int CartPrice { get; set; } //原始價格
        public decimal Discount { get; set; } //折扣，可能為0
        public int FinalCartPrice { get; set; }
        public bool IsFavored { get; set; }
    }
}

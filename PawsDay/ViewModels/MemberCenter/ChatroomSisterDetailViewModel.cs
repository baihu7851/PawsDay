using System.Collections.Generic;
using System;
using PawsDay.Models.MemberCenter;

namespace PawsDay.ViewModels.MemberCenter
{
    public class ChatroomSisterDetailViewModel
    {
        public int SitterId { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
        public string SitterName { get; set; }
        public List<ContactListDTO> Contact { get; set; }
    }
    public class ChatroomSisterDetailDTO
    {
        public int SitterId { get; set; }
        public string Message { get; set; }
    }
    public class ChatroomDetailDTO
    {
        public bool IsSuccess { get; set; }
        public string Time { get; set; }
        public string Image { get; set; }
    }
}

using System;

namespace PawsDay.ViewModels.SitterCenter.DetailData
{
    public class ImageFileData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FileImageUrl { get; set; }
        public DateTimeOffset CreateTime { get; set; }
    }
}

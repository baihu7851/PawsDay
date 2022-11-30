using System;

namespace PawsDay.ViewModels.SitterCenter
{
    public class SitterAptitudeViewModel
    {
        public int MemberID { get; set; }
        public int Extrovert { get; set; }
        public int Introvert { get; set; }
        public int Thinking { get; set; }
        public int Feeling { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }

    
}

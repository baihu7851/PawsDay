using System.Collections.Generic;

namespace PawsDay.Models.SitterCenter
{
    public class SitterChartDto
    {
        public List<string> label { get; set; }
        public List<int> TotalPrice { get; set; }
        public List<int> Ordercount { get; set; }
        public List<int> Customercount { get; set; }
    }
}

using System.Collections.Generic;

namespace PawsDay.ViewModels.Product
{
	public class ChooseTimeDto
	{
        public int Weekday { get; set; }
        public int Time { get; set; }
        public string TimeTitle { get; set; }
        public List<string> TimeDesrcipt { get; set; }
    }
}

using ApplicationCore.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PawsDay.ViewModels.Product
{
	public class ProductChooseDto
	{
        public List<int> Weekday { get; set; }
        public List<ChooseTimeDto> Times { get; set; }
        public List<string> County { get; set; }
        public List<ChooseAreaDto> Areas { get; set; }
        public List<string> PetType { get; set; }
        public List<ChoosePetTypeDto> Types { get; set; }
    }
}
